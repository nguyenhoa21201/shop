using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a71730df-d698-47d9-a7b5-665f44c1a651", "40586ab1-9bb9-4033-a1e8-9110f78ed2a9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74016725-8fe8-4659-ae72-b0f2c2e5fa46", 0, null, "409fe316-877e-4723-8879-68d24d6db1d2", "admin@gmail.com", false, "admin", "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEN2u1f9grjI6NNz4TXGEBaTHuSx/8sAGBBP2EXJh4gyOpTPvdkrlVnShk09B9FAbHg==", "123456789", false, "14f354db-e8c2-451a-94d3-1e42ae3a5125", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a71730df-d698-47d9-a7b5-665f44c1a651", "74016725-8fe8-4659-ae72-b0f2c2e5fa46" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a71730df-d698-47d9-a7b5-665f44c1a651", "74016725-8fe8-4659-ae72-b0f2c2e5fa46" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a71730df-d698-47d9-a7b5-665f44c1a651");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74016725-8fe8-4659-ae72-b0f2c2e5fa46");
        }
    }
}
