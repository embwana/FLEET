using Microsoft.EntityFrameworkCore.Migrations;

namespace FLEET.Data.Migrations
{
    public partial class insurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Station",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Station",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Department_DepartmentId",
                table: "Station",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
