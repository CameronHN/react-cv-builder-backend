using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSkillData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkillType",
                columns: new[] { "Id", "SkillTypeName" },
                values: new object[,]
                {
                    { 1, "Programming Language" },
                    { 2, "Framework" },
                    { 3, "Database" },
                    { 4, "Tool" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "SkillName", "SkillTypeId" },
                values: new object[,]
                {
                    { 1, "C#", 1 },
                    { 2, "JavaScript", 1 },
                    { 3, "Python", 1 },
                    { 4, "React", 2 },
                    { 5, "Angular", 2 },
                    { 6, "SQL Server", 3 },
                    { 7, "MySQL", 3 },
                    { 8, "Git", 4 },
                    { 9, "Docker", 4 },
                    { 10, "Kubernetes", 4 }
                });

            migrationBuilder.InsertData(
                table: "UserSkill",
                columns: new[] { "Id", "SkillId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 4, 1 },
                    { 3, 2, 2 },
                    { 4, 5, 2 },
                    { 5, 3, 3 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 2 },
                    { 9, 9, 2 },
                    { 10, 10, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserSkill",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SkillType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkillType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkillType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkillType",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
