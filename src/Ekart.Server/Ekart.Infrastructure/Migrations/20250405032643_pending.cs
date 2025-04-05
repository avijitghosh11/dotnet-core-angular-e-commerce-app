using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ekart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a21b8e6-1025-4e4a-a65f-2e106f45c996", null, "Customer", "CUSTOMER" },
                    { "c6e8ece8-3116-4246-833c-99b247f2417e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a21b8e6-1025-4e4a-a65f-2e106f45c996");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6e8ece8-3116-4246-833c-99b247f2417e");
        }
    }
}
