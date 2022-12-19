using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9779439-f999-48c2-ab41-50d6828da376", "AQAAAAIAAYagAAAAEKW0RRsFVSLtZMzzeu0fMpDlxNQz4clsqBZrHdaK8wsvjzgPws/n7jQlnUXl6pk3Ag==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "802576be-f1d4-4946-95e0-71eaf9319773", "AQAAAAIAAYagAAAAEJrfvM4rcwBRkLJEUZXaicftrPEqyRaP1caqk+46v+2VHueMZbPxpRT0CmLDJxHPIA==" });
        }
    }
}
