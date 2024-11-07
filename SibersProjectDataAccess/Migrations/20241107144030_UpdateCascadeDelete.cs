using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SibersProjectDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_EmployeeId",
                table: "UserProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_EmployeeId",
                table: "UserProjects",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_EmployeeId",
                table: "UserProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_EmployeeId",
                table: "UserProjects",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
