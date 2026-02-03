using CastlePlus2.Infrastructure.Persistence;
using CastlePlus2.Shared.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CastlePlus2.Api.Controllers.Diagnostics
{
    [ApiController]
    [Route("api/diagnostics")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = RoleCodes.Admin)]
    public sealed class DiagnosticsController : ControllerBase
    {
        private readonly CastlePlus2DbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public DiagnosticsController(CastlePlus2DbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        [HttpGet("db")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDbDiagnostics(CancellationToken ct)
        {
            if (!_environment.IsDevelopment())
                return NotFound();

            var cs = _dbContext.Database.GetConnectionString();
            SqlConnectionStringBuilder? csb = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(cs))
                    csb = new SqlConnectionStringBuilder(cs);
            }
            catch
            {
                // celowo ignorujemy błędy parsera CS w diagnostyce
            }

            var dataSource = csb?.DataSource;
            var initialCatalog = csb?.InitialCatalog;

            string currentDb = string.Empty;
            var zasobUiColumns = new List<string>();
            var duplicates = new List<object>();
            bool? sortSelectable = null;
            string? sortSelectError = null;

            await _dbContext.Database.OpenConnectionAsync(ct);
            try
            {
                DbConnection conn = _dbContext.Database.GetDbConnection();

                // 1) Jaką bazę widzi runtime
                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DB_NAME()";
                    var result = await cmd.ExecuteScalarAsync(ct);
                    currentDb = result?.ToString() ?? string.Empty;
                }

                // 2) Kolumny w [konfiguracja].[ZasobUI]
                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = """
                        SELECT COLUMN_NAME
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_SCHEMA = 'konfiguracja'
                          AND TABLE_NAME = 'ZasobUI'
                        ORDER BY ORDINAL_POSITION
                        """;

                    await using var reader = await cmd.ExecuteReaderAsync(ct);
                    while (await reader.ReadAsync(ct))
                        zasobUiColumns.Add(reader.GetString(0));
                }

                // 3) Czy istnieją duplikaty tabeli ZasobUI w innych schematach (np. dbo)
                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = """
                        SELECT s.name AS SchemaName, t.name AS TableName
                        FROM sys.tables t
                        JOIN sys.schemas s ON s.schema_id = t.schema_id
                        WHERE t.name = 'ZasobUI'
                        ORDER BY s.name
                        """;

                    await using var reader = await cmd.ExecuteReaderAsync(ct);
                    while (await reader.ReadAsync(ct))
                    {
                        duplicates.Add(new
                        {
                            SchemaName = reader.GetString(0),
                            TableName = reader.GetString(1)
                        });
                    }
                }

                // 4) Szybki check: czy można wykonać SELECT TOP(1) [Sort]
                try
                {
                    await using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT TOP(1) [Sort] FROM [konfiguracja].[ZasobUI]";
                    await cmd.ExecuteScalarAsync(ct);
                    sortSelectable = true;
                }
                catch (Exception ex)
                {
                    sortSelectable = false;
                    sortSelectError = ex.Message;
                }
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return Ok(new
            {
                dataSource,
                initialCatalog,
                currentDb,
                zasobUiColumns,
                duplicates,
                sortSelectable,
                sortSelectError
            });
        }
    }
}
