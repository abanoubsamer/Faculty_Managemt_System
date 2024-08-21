using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domin.Migrations
{
    /// <inheritdoc />
    public partial class updatesection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Section_Name_DepartmentId",
                table: "Section");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Name_DepartmentId_LevelId",
                table: "Section",
                columns: new[] { "Name", "DepartmentId", "LevelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Section_Name_DepartmentId_LevelId",
                table: "Section");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Name_DepartmentId",
                table: "Section",
                columns: new[] { "Name", "DepartmentId" },
                unique: true);
        }
    }
}
