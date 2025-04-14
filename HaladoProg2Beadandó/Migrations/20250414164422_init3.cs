using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CryptoAssets",
                newName: "Symbol");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "CryptoAssets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CryptoCurrencyName",
                table: "CryptoAssets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CryptoAssets");

            migrationBuilder.DropColumn(
                name: "CryptoCurrencyName",
                table: "CryptoAssets");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "CryptoAssets",
                newName: "Name");
        }
    }
}
