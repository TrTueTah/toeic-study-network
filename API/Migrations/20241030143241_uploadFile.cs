using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class uploadFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743b1d07-2e0e-4e6f-84f0-ae65bbebf820");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb018ea9-199e-4795-840c-c2dd5f896532");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrls",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d962710-0c87-4f1a-a015-fb17c778ddbb", null, "User", "USER" },
                    { "f9fb9cc2-a5ab-4e11-abcb-b8b849e6ca17", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d962710-0c87-4f1a-a015-fb17c778ddbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9fb9cc2-a5ab-4e11-abcb-b8b849e6ca17");

            migrationBuilder.DropColumn(
                name: "MediaUrls",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "743b1d07-2e0e-4e6f-84f0-ae65bbebf820", null, "User", "USER" },
                    { "eb018ea9-199e-4795-840c-c2dd5f896532", null, "Admin", "ADMIN" }
                });
        }
    }
}
