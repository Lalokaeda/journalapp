using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journalapp.Migrations
{
    /// <inheritdoc />
    public partial class newUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a58eada7-04a7-4e41-be37-7cfb5c9689b7", "AQAAAAIAAYagAAAAEGtxSNINAUfWAxPhTIsFiZe7lOTmbRumrhWs5LAD1lj40TeDtjL89FKfn+ukxcZLSg==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ClassTeacherId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3b62422e-4a16-45fa-a20f-e7685b9565s9", 0, null, "c65bd3aa-4417-4bf4-ae22-d73cc9931f47", "vlastadev@gmail.com", true, false, null, "VLASTADEV@GMAIL.COM", "TUTAROVA_V", "AQAAAAIAAYagAAAAEKIxtsHVCXfyM0DnScXCQKrsTtlDgP4uBB/tRqDtxDL0zHBjEHJpd9+GvpaH8OQbZg==", null, false, "", false, "Tutarova_v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62422e-4a16-45fa-a20f-e7685b9565s9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "72593db3-2b5b-44fd-8137-84e71861809d", "AQAAAAIAAYagAAAAEA94KQur9Hh2vXXNa6LGNrz0tDM03IImpdb9ivaSzpi8GMTKVov/N43bKX3Dj7J7Kw==" });
        }
    }
}
