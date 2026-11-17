using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutItems");
        }
    }
}
