using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateDetailResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailResults_UserResults_UserResultId",
                table: "DetailResults");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailResults_UserResults_UserResultId",
                table: "DetailResults",
                column: "UserResultId",
                principalTable: "UserResults",
                principalColumn: "UserResultId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailResults_UserResults_UserResultId",
                table: "DetailResults");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailResults_UserResults_UserResultId",
                table: "DetailResults",
                column: "UserResultId",
                principalTable: "UserResults",
                principalColumn: "UserResultId");
        }
    }
}
