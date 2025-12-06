using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace CastlePlus2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rdzen");

            migrationBuilder.CreateTable(
                name: "Nieruchomosc",
                schema: "rdzen",
                columns: table => new
                {
                    IdEncji = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdAdresuGlownego = table.Column<long>(type: "bigint", nullable: true),
                    IdPodmiotuWlasciciela = table.Column<long>(type: "bigint", nullable: true),
                    Geometria = table.Column<Geometry>(type: "geography", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nieruchomosc", x => x.IdEncji);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nieruchomosc",
                schema: "rdzen");
        }
    }
}
