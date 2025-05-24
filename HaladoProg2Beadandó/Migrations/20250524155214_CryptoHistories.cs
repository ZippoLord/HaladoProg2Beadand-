using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    /// <inheritdoc />
    public partial class CryptoHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoPriceHistories",
                columns: table => new
                {
                    CryptoPriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoCurrencyId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoPriceHistories", x => x.CryptoPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_CryptoPriceHistories_CryptoCurrencies_CryptoCurrencyId",
                        column: x => x.CryptoCurrencyId,
                        principalTable: "CryptoCurrencies",
                        principalColumn: "CryptoCurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoPriceHistories_CryptoCurrencyId",
                table: "CryptoPriceHistories",
                column: "CryptoCurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoPriceHistories");
        }
    }
}
