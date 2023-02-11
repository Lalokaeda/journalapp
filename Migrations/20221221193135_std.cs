using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class std : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Students",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_StudentId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "72593db3-2b5b-44fd-8137-84e71861809d", "AQAAAAIAAYagAAAAEA94KQur9Hh2vXXNa6LGNrz0tDM03IImpdb9ivaSzpi8GMTKVov/N43bKX3Dj7J7Kw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_PositionId",
                table: "Students",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Positions_PositionId",
                table: "Students",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Positions_PositionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PositionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9c430c2a-08b4-45f6-afd1-7ecaf303778b", "AQAAAAIAAYagAAAAEB/zLtNvQaU/9UAgjItKo41yMlAWfyOCx3TtqvOfujSXVSs4jlstKYAINhpPQ7QDUA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_StudentId",
                table: "Positions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Students",
                table: "Positions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
