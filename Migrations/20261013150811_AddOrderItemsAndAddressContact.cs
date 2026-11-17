using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVanChuyenLogicstic.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemsAndAddressContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "SavedAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "SavedAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CargoTypeId = table.Column<int>(type: "int", nullable: true),
                    CargoTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_CargoTypes_CargoTypeId",
                        column: x => x.CargoTypeId,
                        principalTable: "CargoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CargoTypeId",
                table: "OrderItems",
                column: "CargoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
            migrationBuilder.Sql(@"
            INSERT INTO OrderItems (OrderId, CargoTypeId, CargoTypeName, Quantity, WeightKg, IsFragile)
            SELECT o.Id, c.Id, LEFT(o.CargoType, 100),
                   ISNULL(NULLIF(o.PackageCount, 0), 1), o.WeightKg, ISNULL(c.IsSpecial, 0)
            FROM Orders o
            LEFT JOIN CargoTypes c ON c.Name = o.CargoType
            WHERE o.CargoType IS NOT NULL AND o.CargoType <> '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "SavedAddresses");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "SavedAddresses");
        }
    }
}
