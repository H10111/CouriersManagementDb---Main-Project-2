using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouriersManagementDb.Migrations
{
    /// <inheritdoc />
    public partial class CourierPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Couriers",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Couriers",
                newName: "Contact");
        }
    }
}
