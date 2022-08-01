using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class AddLocationCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationCodeId",
                schema: "usr",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationCode",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByUserId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
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
                name: "IX_Ticket_LocationCodeId",
                schema: "usr",
                table: "Ticket",
                column: "LocationCodeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_LocationCodeId_LocationCode_Id",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "LocationCode",
                schema: "lst");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_LocationCodeId",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "LocationCodeId",
                schema: "usr",
                table: "Ticket");
        }
    }
}
