using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class dddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dfe660dd-00d9-48e2-8e80-c5e799bdabc0", "AQAAAAIAAYagAAAAEM5+y+/6M4mWuLudxlUq6AnLKrVKz9wUCxCicklfk+tox4kTEg+sUj9uibWxQdrLxw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a849011c-abbc-4859-a4b7-4fa17d11733b", "AQAAAAIAAYagAAAAEAppP3KMmMbckQa2lPFhIIzBgRVuLzVYDCCcxCus+uZiIi/bu7UqNrJKtkwoQhuNgg==" });
        }
    }
}
