using Microsoft.EntityFrameworkCore.Migrations;

namespace Synel.Infrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "Forenames");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Employees",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Address_2",
                table: "Employees",
                newName: "Address2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Forenames",
                table: "Employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Employees",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Employees",
                newName: "Address_2");
        }
    }
}
