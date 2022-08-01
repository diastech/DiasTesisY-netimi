using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class AddMobileMenuPageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileMenuPage",
                schema: "lst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchicalOrder = table.Column<int>(type: "int", nullable: false),
                    HierarchicalLevel = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", maxLength: 255, nullable: true),
                    MenuText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MenuIcon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpandOnStart = table.Column<bool>(type: "bit", nullable: false),
                    MenuImagePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AuthorizationCodeLevel = table.Column<long>(type: "bigint", nullable: false),
                    AuthorizationCode = table.Column<long>(type: "bigint", nullable: false),
                    AddedByUserId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    AddedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileMenuPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileMenuPage_AddedByUserId_User_Id",
                        column: x => x.AddedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MobileMenuPage_LastModifiedByUserId_User_Id",
                        column: x => x.LastModifiedByUserId,
                        principalSchema: "idn",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MobileMenuPage_ParentId_MobileMenuPage_Id",
                        column: x => x.ParentId,
                        principalSchema: "lst",
                        principalTable: "MobileMenuPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileMenuPage_AddedByUserId",
                schema: "lst",
                table: "MobileMenuPage",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileMenuPage_LastModifiedByUserId",
                schema: "lst",
                table: "MobileMenuPage",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileMenuPage_ParentId",
                schema: "lst",
                table: "MobileMenuPage",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileMenuPage",
                schema: "lst");
        }
    }
}
