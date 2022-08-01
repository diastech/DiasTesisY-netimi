using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class ChangeLocationCodeTableRemoveFacilityTablev8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
