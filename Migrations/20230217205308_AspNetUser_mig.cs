using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class AspNetUsermig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Positions_PositionId",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62422e-4a16-45fa-a20f-e7685b9565s9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cb3d3bbe-9cd9-4ee0-9224-1b1a966fc8c2", "AQAAAAIAAYagAAAAELq6Yzp7Hjzdt3FNdaiULGcF9Oz9kOGRQ6ZkYvNW2U05TSRKSIF4D+OdLred/PWLrQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e774dab2-c5f5-4380-8572-afcabd0f5d66", "AQAAAAIAAYagAAAAEEEIh8zDaZNJO+L6ptOiJbFXETBzYxBMWxZTItVySFEV2ify5TyEfnx4+ZwQVT5dmQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Positions",
                table: "Students",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Students_Positions",
                table: "Students",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62422e-4a16-45fa-a20f-e7685b9565s9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c65bd3aa-4417-4bf4-ae22-d73cc9931f47", "AQAAAAIAAYagAAAAEKIxtsHVCXfyM0DnScXCQKrsTtlDgP4uBB/tRqDtxDL0zHBjEHJpd9+GvpaH8OQbZg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a58eada7-04a7-4e41-be37-7cfb5c9689b7", "AQAAAAIAAYagAAAAEGtxSNINAUfWAxPhTIsFiZe7lOTmbRumrhWs5LAD1lj40TeDtjL89FKfn+ukxcZLSg==" });

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Positions_PositionId",
                table: "Students");
        }
    }
}
