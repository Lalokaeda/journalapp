using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class fffd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a849011c-abbc-4859-a4b7-4fa17d11733b", "AQAAAAIAAYagAAAAEAppP3KMmMbckQa2lPFhIIzBgRVuLzVYDCCcxCus+uZiIi/bu7UqNrJKtkwoQhuNgg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9779439-f999-48c2-ab41-50d6828da376", "AQAAAAIAAYagAAAAEKW0RRsFVSLtZMzzeu0fMpDlxNQz4clsqBZrHdaK8wsvjzgPws/n7jQlnUXl6pk3Ag==" });
        }
    }
}
