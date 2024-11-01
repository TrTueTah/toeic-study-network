using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class likePostHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d962710-0c87-4f1a-a015-fb17c778ddbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9fb9cc2-a5ab-4e11-abcb-b8b849e6ca17");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83fd3fa9-d9c7-4e04-9a61-fbc2cf92aee7", null, "Admin", "ADMIN" },
                    { "95bcc922-1d4e-4851-8e3c-ae5bb093d19f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83fd3fa9-d9c7-4e04-9a61-fbc2cf92aee7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95bcc922-1d4e-4851-8e3c-ae5bb093d19f");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d962710-0c87-4f1a-a015-fb17c778ddbb", null, "User", "USER" },
                    { "f9fb9cc2-a5ab-4e11-abcb-b8b849e6ca17", null, "Admin", "ADMIN" }
                });
        }
    }
}
