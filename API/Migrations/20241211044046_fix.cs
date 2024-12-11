using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExamSeriesId",
                table: "Exams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_UserResults_ExamId",
                table: "UserResults",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResults_Exams_ExamId",
                table: "UserResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResults_Exams_ExamId",
                table: "UserResults");

            migrationBuilder.DropIndex(
                name: "IX_UserResults_ExamId",
                table: "UserResults");

            migrationBuilder.AlterColumn<string>(
                name: "ExamSeriesId",
                table: "Exams",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
