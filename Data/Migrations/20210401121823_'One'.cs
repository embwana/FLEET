using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FLEET.Data.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "COF",
                columns: table => new
                {
                    COFId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COFNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Issued = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COF", x => x.COFId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(maxLength: 50, nullable: false),
                    Comment = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_Department_DepartmentId1",
                        column: x => x.DepartmentId1,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProvider",
                columns: table => new
                {
                    InsuranceProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(maxLength: 50, nullable: false),
                    ServiceOffered = table.Column<string>(maxLength: 50, nullable: false),
                    EstablishedYear = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProvider", x => x.InsuranceProviderId);
                });

            migrationBuilder.CreateTable(
                name: "Licences",
                columns: table => new
                {
                    LicenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 50, nullable: false),
                    LicenceNumber = table.Column<string>(maxLength: 50, nullable: false),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    RenewedDate = table.Column<DateTime>(nullable: false),
                    Department = table.Column<string>(maxLength: 50, nullable: false),
                    ProfilePicture = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licences", x => x.LicenceId);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    StationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(maxLength: 50, nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    FleetNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Station_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceNumber = table.Column<string>(nullable: false),
                    Details = table.Column<string>(maxLength: 100, nullable: false),
                    Issued = table.Column<DateTime>(nullable: false),
                    renewed = table.Column<DateTime>(nullable: false),
                    InsuranceProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.InsuranceId);
                    table.ForeignKey(
                        name: "FK_Insurance_InsuranceProvider_InsuranceProviderId",
                        column: x => x.InsuranceProviderId,
                        principalTable: "InsuranceProvider",
                        principalColumn: "InsuranceProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FleetCategory",
                columns: table => new
                {
                    FleetCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberPlate = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<DateTime>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    TagNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Mileage = table.Column<string>(maxLength: 50, nullable: false),
                    COFId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: true),
                    COMUL = table.Column<int>(nullable: false),
                    InsuranceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleetCategory", x => x.FleetCategoryId);
                    table.ForeignKey(
                        name: "FK_FleetCategory_COF_COFId",
                        column: x => x.COFId,
                        principalTable: "COF",
                        principalColumn: "COFId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCategory_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCategory_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCategory_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FleetCustodian",
                columns: table => new
                {
                    FleetCustodianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenceId = table.Column<int>(nullable: true),
                    FleetCategoryId = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    CollectedOn = table.Column<DateTime>(nullable: false),
                    ReturnedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleetCustodian", x => x.FleetCustodianId);
                    table.ForeignKey(
                        name: "FK_FleetCustodian_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCustodian_FleetCategory_FleetCategoryId",
                        column: x => x.FleetCategoryId,
                        principalTable: "FleetCategory",
                        principalColumn: "FleetCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCustodian_Licences_LicenceId",
                        column: x => x.LicenceId,
                        principalTable: "Licences",
                        principalColumn: "LicenceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCustodian_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FleetSize",
                columns: table => new
                {
                    FleetSizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleetCategoryId = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleetSize", x => x.FleetSizeId);
                    table.ForeignKey(
                        name: "FK_FleetSize_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetSize_FleetCategory_FleetCategoryId",
                        column: x => x.FleetCategoryId,
                        principalTable: "FleetCategory",
                        principalColumn: "FleetCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetSize_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grounded",
                columns: table => new
                {
                    GroundedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleetCategoryId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grounded", x => x.GroundedId);
                    table.ForeignKey(
                        name: "FK_Grounded_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grounded_FleetCategory_FleetCategoryId",
                        column: x => x.FleetCategoryId,
                        principalTable: "FleetCategory",
                        principalColumn: "FleetCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grounded_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentId1",
                table: "Department",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCategory_COFId",
                table: "FleetCategory",
                column: "COFId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCategory_DepartmentId",
                table: "FleetCategory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCategory_InsuranceId",
                table: "FleetCategory",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCategory_StationId",
                table: "FleetCategory",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCustodian_DepartmentId",
                table: "FleetCustodian",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCustodian_FleetCategoryId",
                table: "FleetCustodian",
                column: "FleetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCustodian_LicenceId",
                table: "FleetCustodian",
                column: "LicenceId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCustodian_StationId",
                table: "FleetCustodian",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetSize_DepartmentId",
                table: "FleetSize",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetSize_FleetCategoryId",
                table: "FleetSize",
                column: "FleetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetSize_StationId",
                table: "FleetSize",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Grounded_DepartmentId",
                table: "Grounded",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grounded_FleetCategoryId",
                table: "Grounded",
                column: "FleetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Grounded_StationId",
                table: "Grounded",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_InsuranceProviderId",
                table: "Insurance",
                column: "InsuranceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_DepartmentId",
                table: "Station",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FleetCustodian");

            migrationBuilder.DropTable(
                name: "FleetSize");

            migrationBuilder.DropTable(
                name: "Grounded");

            migrationBuilder.DropTable(
                name: "Licences");

            migrationBuilder.DropTable(
                name: "FleetCategory");

            migrationBuilder.DropTable(
                name: "COF");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "InsuranceProvider");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
