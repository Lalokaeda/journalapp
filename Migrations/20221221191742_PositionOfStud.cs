using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class PositionOfStud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9c430c2a-08b4-45f6-afd1-7ecaf303778b", "AQAAAAIAAYagAAAAEB/zLtNvQaU/9UAgjItKo41yMlAWfyOCx3TtqvOfujSXVSs4jlstKYAINhpPQ7QDUA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "399170d6-12ad-4ba3-a19c-3e2dd1ba7c45", "AQAAAAIAAYagAAAAEFP9zD/tT74Wp/GDQfvOnG802gtpQk70ZHF40PVwxxFQCFAnKjImlFAr8DgobSMo2A==" });
        }
    }
}
