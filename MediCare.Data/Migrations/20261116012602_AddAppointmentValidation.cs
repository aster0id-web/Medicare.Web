using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-uid",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fe5d9d0d-3483-4419-9b49-b59e9dd74a49", "418a2fda-2fb3-408d-afd3-987c2d7a1ffd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-uid",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f8dfeab-e31a-4fa6-92bb-b77e52562762", "611be18f-3bbb-44d2-b721-9f3b3808dd0e" });
        }
    }
}
