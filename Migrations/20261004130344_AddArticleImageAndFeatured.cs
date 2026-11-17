using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddArticleImageAndFeatured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeaturedOrder",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedOrder",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Articles");
        }
    }
}
