using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    /// <inheritdoc />
    public partial class VirtualInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VirtualWallets",
                newName: "VirtualWalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VirtualWalletId",
                table: "VirtualWallets",
                newName: "Id");
        }
    }
}
