using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouriersManagementDb.Migrations
{
    /// <inheritdoc />
    public partial class Enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_Couriers_CourierID",
                table: "Pallets");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryStatus",
                table: "Shipments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourierID",
                table: "Pallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_Couriers_CourierID",
                table: "Pallets",
                column: "CourierID",
                principalTable: "Couriers",
                principalColumn: "CourierID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_Couriers_CourierID",
                table: "Pallets");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryStatus",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourierID",
                table: "Pallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_Couriers_CourierID",
                table: "Pallets",
                column: "CourierID",
                principalTable: "Couriers",
                principalColumn: "CourierID");
        }
    }
}
