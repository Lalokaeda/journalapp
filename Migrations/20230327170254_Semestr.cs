using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class Semestr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semester",
                table: "Business",
                newName: "Semestr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semestr",
                table: "Business",
                newName: "Semester");
        }
    }
}
