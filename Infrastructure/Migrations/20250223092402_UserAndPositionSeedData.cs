using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAndPositionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "joh@example.com", "John", "Doe", "Henry", "" },
                    { 2, "jane@email.com", "Jane", "Doe", "", "1234567890" },
                    { 3, "kev@email.com", "Kevin", "Kelvin", "James", "0987654321" },
                    { 4, "charlie@email.com", "Charlie", "Thompson", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "EndDate", "Role", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, "2021-01-01", "Software Developer Intern", "2020-01-01", 1 },
                    { 2, "2022-01-01", "Software Developer", "2021-01-02", 1 },
                    { 3, "Present", "Junior Software Engineer", "2022-01-02", 1 },
                    { 4, "2021-01-01", "Software Developer", "2020-01-01", 2 },
                    { 5, "Present", "Mechanical Mathematician", "2021-01-02", 2 },
                    { 6, "Present", "Astrophysicist", "2020-01-01", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_UserId",
                table: "Positions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Users_UserId",
                table: "Positions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Users_UserId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_UserId",
                table: "Positions");

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
