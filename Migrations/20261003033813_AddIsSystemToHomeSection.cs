using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSystemToHomeSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "HomeSections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "HomeSections");
        }
    }
}
