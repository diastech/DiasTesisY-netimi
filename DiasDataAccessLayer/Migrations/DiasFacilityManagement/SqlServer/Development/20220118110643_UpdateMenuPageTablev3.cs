using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class UpdateMenuPageTablev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2Id_MenuPageV2_Id",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.RenameColumn(
                name: "MenuPageId",
                schema: "idn",
                table: "CompanyRoleClaim",
                newName: "WebMenuPageV2Level");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                schema: "lst",
                table: "MenuPage",
                type: "int",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobilMenuPageLevel",
                schema: "idn",
                table: "CompanyRoleClaim",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WebMenuPageLevel",
                schema: "idn",
                table: "CompanyRoleClaim",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuPage_ParentId",
                schema: "lst",
                table: "MenuPage",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoleClaim_RestClientTypeId",
                schema: "idn",
                table: "CompanyRoleClaim",
                column: "RestClientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2_MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim",
                column: "MenuPageV2Id",
                principalSchema: "lst",
                principalTable: "MenuPageV2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRoleClaim_RestClientTypeId_RestClientType_Id",
                schema: "idn",
                table: "CompanyRoleClaim",
                column: "RestClientTypeId",
                principalSchema: "lst",
                principalTable: "RestClientType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPage_ParentId_MenuPage_Id",
                schema: "lst",
                table: "MenuPage",
                column: "ParentId",
                principalSchema: "lst",
                principalTable: "MenuPage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2_MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRoleClaim_RestClientTypeId_RestClientType_Id",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPage_ParentId_MenuPage_Id",
                schema: "lst",
                table: "MenuPage");

            migrationBuilder.DropIndex(
                name: "IX_MenuPage_ParentId",
                schema: "lst",
                table: "MenuPage");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRoleClaim_RestClientTypeId",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropColumn(
                name: "MobilMenuPageLevel",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropColumn(
                name: "WebMenuPageLevel",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.RenameColumn(
                name: "WebMenuPageV2Level",
                schema: "idn",
                table: "CompanyRoleClaim",
                newName: "MenuPageId");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                schema: "lst",
                table: "MenuPage",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2Id_MenuPageV2_Id",
                schema: "idn",
                table: "CompanyRoleClaim",
                column: "MenuPageV2Id",
                principalSchema: "lst",
                principalTable: "MenuPageV2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
