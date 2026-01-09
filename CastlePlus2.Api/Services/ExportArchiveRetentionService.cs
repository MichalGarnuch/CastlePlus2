using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Contracts.Exports;
using CastlePlus2.Infrastructure.Services.Exports;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Api.Services;

public sealed class ExportArchiveRetentionService : BackgroundService
{
    private static readonly TimeSpan RunTime = new(2, 0, 0);
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExportArchiveRetentionService> _logger;
    private readonly ExportStorageOptions _options;
    private readonly IConfiguration _configuration;

    public ExportArchiveRetentionService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExportArchiveRetentionService> logger,
        ExportStorageOptions options,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _options = options;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Usługa retencji eksportów uruchomiona.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTimeOffset.Now;
            var nextRun = GetNextRun(now);
            var delay = nextRun - now;

            if (delay < TimeSpan.Zero)
                delay = TimeSpan.Zero;

            _logger.LogInformation("Następne czyszczenie eksportów zaplanowane na {NextRun}.", nextRun);

            try
            {
                await Task.Delay(delay, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }

            if (stoppingToken.IsCancellationRequested)
                break;

            if (_options.RetentionDays <= 0)
            {
                _logger.LogInformation("Pominięto retencję eksportów (RetentionDays <= 0).");
                continue;
            }

            _logger.LogInformation("Start retencji eksportów (dni: {RetentionDays}).", _options.RetentionDays);

            using var scope = _scopeFactory.CreateScope();
            var archiveService = scope.ServiceProvider.GetRequiredService<IExportArchiveService>();
            await archiveService.DeleteOlderThanAsync(_options.RetentionDays, stoppingToken);

            _logger.LogInformation("Zakończono retencję eksportów.");
        }

        _logger.LogInformation("Usługa retencji eksportów zatrzymana.");
    }


    private static DateTimeOffset GetNextRun(DateTimeOffset now)
    {
        var todayRun = new DateTimeOffset(now.Year, now.Month, now.Day, RunTime.Hours, RunTime.Minutes, 0, now.Offset);
        return now >= todayRun ? todayRun.AddDays(1) : todayRun;
    }
}