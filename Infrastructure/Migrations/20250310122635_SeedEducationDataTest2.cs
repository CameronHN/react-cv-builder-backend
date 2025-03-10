using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEducationDataTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "Id", "EndDate", "FieldOfStudy", "Institution", "Major", "Qualification", "StartDate", "UserId" },
                values: new object[] { 1, "2018-12-31", "Comp Sci", "Univ of Example", "Software Eng", "BSc", "2015-01-01", 1 });

            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "Id", "EndDate", "FieldOfStudy", "Institution", "Major", "Qualification", "StartDate", "UserId" },
                values: new object[] { 2, "2020-12-31", "IT", "Example Inst of Tech", "Computer Science", "MSc", "2019-01-01", 1 });

            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "Id", "EndDate", "FieldOfStudy", "Institution", "Major", "Qualification", "StartDate", "UserId" },
                values: new object[] { 3, "2020-12-31", "IT", "Example Inst of Tech", "Computer Science", "ITD", "2019-01-01", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
