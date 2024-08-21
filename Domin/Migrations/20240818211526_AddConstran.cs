using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domin.Migrations
{
    /// <inheritdoc />
    public partial class AddConstran : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachTo_ProfessorsId",
                table: "TeachTo");

            migrationBuilder.DropIndex(
                name: "IX_TeachBy_SectionId",
                table: "TeachBy");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentsId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_CourseDepartment_DepartmentsId",
                table: "CourseDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_TeachTo_ProfessorsId_CoursesId",
                table: "TeachTo",
                columns: new[] { "ProfessorsId", "CoursesId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachBy_SectionId_CourseId",
                table: "TeachBy",
                columns: new[] { "SectionId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentsId_CoursesId",
                table: "Enrollment",
                columns: new[] { "StudentsId", "CoursesId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseDepartment_DepartmentsId_CoursesId",
                table: "CourseDepartment",
                columns: new[] { "DepartmentsId", "CoursesId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachTo_ProfessorsId_CoursesId",
                table: "TeachTo");

            migrationBuilder.DropIndex(
                name: "IX_TeachBy_SectionId_CourseId",
                table: "TeachBy");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentsId_CoursesId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_CourseDepartment_DepartmentsId_CoursesId",
                table: "CourseDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_TeachTo_ProfessorsId",
                table: "TeachTo",
                column: "ProfessorsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachBy_SectionId",
                table: "TeachBy",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentsId",
                table: "Enrollment",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseDepartment_DepartmentsId",
                table: "CourseDepartment",
                column: "DepartmentsId");
        }
    }
}
