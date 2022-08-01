using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class ChangeLocationCodeTableRemoveFacilityTablev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_LocationCodeId_LocationCode_Id",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "LocationCode",
                schema: "lst");

            //migrationBuilder.DropTable(
            //   name: "Facility",
            //   schema: "lst");

            migrationBuilder.RenameColumn(
                name: "LocationCodeId",
                schema: "usr",
                table: "Ticket",
                newName: "FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_LocationCodeId",
                schema: "usr",
                table: "Ticket",
                newName: "IX_Ticket_FacilityId");

            migrationBuilder.CreateTable(
                name: "Facility",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facility_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facility_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facility_AddedByUserId",
                schema: "lst",
                table: "Facility",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Facility_LastModifiedByUserId",
                schema: "lst",
                table: "Facility",
                column: "LastModifiedByUserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Ticket_FacilityId_Facility_Id",
            //    schema: "usr",
            //    table: "Ticket",
            //    column: "FacilityId",
            //    principalSchema: "lst",
            //    principalTable: "Facility",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_FacilityId_Facility_Id",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Facility",
                schema: "lst");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                schema: "usr",
                table: "Ticket",
                newName: "LocationCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_FacilityId",
                schema: "usr",
                table: "Ticket",
                newName: "IX_Ticket_LocationCodeId");

            migrationBuilder.CreateTable(
                name: "LocationCode",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedByUserId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationCode_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationCode_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationCode_AddedByUserId",
                schema: "lst",
                table: "LocationCode",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCode_LastModifiedByUserId",
                schema: "lst",
                table: "LocationCode",
                column: "LastModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_LocationCodeId_LocationCode_Id",
                schema: "usr",
                table: "Ticket",
                column: "LocationCodeId",
                principalSchema: "lst",
                principalTable: "LocationCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
