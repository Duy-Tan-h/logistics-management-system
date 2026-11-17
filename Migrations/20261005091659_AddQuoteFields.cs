using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuoteNote",
                table: "Orders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteToken",
                table: "Orders",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuoteValidUntil",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QuotedPrice",
                table: "Orders",
                type: "decimal(18,0)",
                precision: 18,
                scale: 0,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_QuoteToken",
                table: "Orders",
                column: "QuoteToken",
                unique: true,
                filter: "[QuoteToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_QuoteToken",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuoteNote",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuoteToken",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuoteValidUntil",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuotedPrice",
                table: "Orders");
        }
    }
}
