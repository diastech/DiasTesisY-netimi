using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class UpdateCompanyRoleClaimTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2_MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRoleClaim_MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim");

            migrationBuilder.DropColumn(
                name: "MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoleClaim_MenuPageV2Id",
                schema: "idn",
                table: "CompanyRoleClaim",
                column: "MenuPageV2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRoleClaim_MenuPageV2_MenuPageV2Id",
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
