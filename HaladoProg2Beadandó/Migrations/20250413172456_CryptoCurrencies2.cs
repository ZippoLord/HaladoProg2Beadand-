﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    /// <inheritdoc />
    public partial class CryptoCurrencies2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CryptoCurrencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CryptoCurrencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
