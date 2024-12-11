using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamSeries_ExamSeriesId",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamSeries_ExamSeriesId",
                table: "Exams",
                column: "ExamSeriesId",
                principalTable: "ExamSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamSeries_ExamSeriesId",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamSeries_ExamSeriesId",
                table: "Exams",
                column: "ExamSeriesId",
                principalTable: "ExamSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
