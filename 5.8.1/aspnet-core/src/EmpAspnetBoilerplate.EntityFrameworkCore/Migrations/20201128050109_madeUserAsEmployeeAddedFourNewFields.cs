using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpAspnetBoilerplate.Migrations
{
    public partial class madeUserAsEmployeeAddedFourNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "AbpUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Departments_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Departments_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "AbpUsers");
        }
    }
}
