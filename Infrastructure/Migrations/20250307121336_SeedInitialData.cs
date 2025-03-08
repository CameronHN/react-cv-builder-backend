using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John", "Doe", "Henry", "" },
                    { 2, "jane@email.com", "Jane", "Doe", "", "1234567890" },
                    { 3, "kev@email.com", "Kevin", "Kelvin", "James", "0987654321" },
                    { 4, "charlie@email.com", "Charlie", "Thompson", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "EndDate", "Role", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, "2021-01-01", "Software Developer Intern", "2020-01-01", 1 },
                    { 2, "2022-01-01", "Software Developer", "2021-01-02", 1 },
                    { 3, "", "Junior Software Engineer", "2022-01-02", 1 },
                    { 4, "2021-01-01", "Software Developer", "2020-01-01", 2 },
                    { 5, "", "Mechanical Mathematician", "2021-01-02", 2 },
                    { 6, "", "Astrophysicist", "2020-01-01", 3 }
                });

            migrationBuilder.InsertData(
                table: "PositionResponsibility",
                columns: new[] { "Id", "PositionId", "Responsibility" },
                values: new object[,]
                {
                    { 1, 1, "Assist in software development tasks" },
                    { 2, 1, "Write and maintain code" },
                    { 3, 1, "Collaborate with team members" },
                    { 4, 1, "Participate in code reviews" },
                    { 5, 1, "Assist in testing and debugging" },
                    { 6, 2, "Develop new software features" },
                    { 7, 2, "Maintain existing codebase" },
                    { 8, 2, "Collaborate with cross-functional teams" },
                    { 9, 2, "Review and refactor code" },
                    { 10, 2, "Document development processes" },
                    { 11, 3, "Assist in software design" },
                    { 12, 3, "Implement software solutions" },
                    { 13, 3, "Collaborate with developers" },
                    { 14, 3, "Participate in Agile processes" },
                    { 15, 3, "Assist in system testing" },
                    { 16, 4, "Develop software components" },
                    { 17, 4, "Maintain technical documentation" },
                    { 18, 4, "Work with QA team" },
                    { 19, 4, "Review pull requests" },
                    { 20, 4, "Fix software bugs" },
                    { 21, 5, "Conduct mathematical analysis" },
                    { 22, 5, "Develop mathematical models" },
                    { 23, 5, "Collaborate with engineering teams" },
                    { 24, 5, "Present findings and recommendations" },
                    { 25, 5, "Document mathematical solutions" },
                    { 26, 6, "Conduct astrophysical research" },
                    { 27, 6, "Analyze astronomical data" },
                    { 28, 6, "Collaborate with research teams" },
                    { 29, 6, "Present research findings" },
                    { 30, 6, "Publish research papers" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PositionResponsibility",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
