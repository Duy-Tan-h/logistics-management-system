using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceFieldsAndCargoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "CargoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypes_Name",
                table: "CargoTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Services");
        }
    }
}
