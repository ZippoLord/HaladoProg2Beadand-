using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoAssets",
                columns: table => new
                {
                    CryptoAssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    VirtualWalletId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAssets", x => x.CryptoAssetId);
                    table.ForeignKey(
                        name: "FK_CryptoAssets_VirtualWallets_VirtualWalletId",
                        column: x => x.VirtualWalletId,
                        principalTable: "VirtualWallets",
                        principalColumn: "VirtualWalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAssets_VirtualWalletId",
                table: "CryptoAssets",
                column: "VirtualWalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoAssets");
        }
    }
}
