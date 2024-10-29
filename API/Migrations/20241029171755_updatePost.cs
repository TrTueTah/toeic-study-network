using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2cd05d-4cdf-42bf-b18c-385c593c1a54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eae7e886-1474-49bf-a90e-6276997b8ad3");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "743b1d07-2e0e-4e6f-84f0-ae65bbebf820", null, "User", "USER" },
                    { "eb018ea9-199e-4795-840c-c2dd5f896532", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Icon",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bd2cd05d-4cdf-42bf-b18c-385c593c1a54", null, "Admin", "ADMIN" },
                    { "eae7e886-1474-49bf-a90e-6276997b8ad3", null, "User", "USER" }
                });
        }
    }
}
