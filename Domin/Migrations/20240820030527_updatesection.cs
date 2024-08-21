using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domin.Migrations
{
    /// <inheritdoc />
    public partial class updatesection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Section",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Section_LevelId",
                table: "Section",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Levels_LevelId",
                table: "Section",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Levels_LevelId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Section_LevelId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Section");
        }
    }
}
