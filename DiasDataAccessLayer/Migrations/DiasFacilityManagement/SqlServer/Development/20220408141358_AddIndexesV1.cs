using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class AddIndexesV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Index_Unique_Clustered_User_Email",
                schema: "idn",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "idn",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "Index_Unique_Clustered_TicketReasonCategoryV2_HierarchyId",
                schema: "adm",
                table: "TicketReasonCategoryV2",
                column: "HierarchyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketReasonCategoryV2_HierarchyId",
                schema: "adm",
                table: "TicketReasonCategoryV2",
                column: "HierarchyId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "Index_Unique_Clustered_LocationV2_HierarchyId",
                schema: "lst",
                table: "LocationV2",
                column: "HierarchyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationV2_HierarchyId",
                schema: "lst",
                table: "LocationV2",
                column: "HierarchyId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Unique_Clustered_User_Email",
                schema: "idn",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                schema: "idn",
                table: "User");

            migrationBuilder.DropIndex(
                name: "Index_Unique_Clustered_TicketReasonCategoryV2_HierarchyId",
                schema: "adm",
                table: "TicketReasonCategoryV2");

            migrationBuilder.DropIndex(
                name: "IX_TicketReasonCategoryV2_HierarchyId",
                schema: "adm",
                table: "TicketReasonCategoryV2");

            migrationBuilder.DropIndex(
                name: "Index_Unique_Clustered_LocationV2_HierarchyId",
                schema: "lst",
                table: "LocationV2");

            migrationBuilder.DropIndex(
                name: "IX_LocationV2_HierarchyId",
                schema: "lst",
                table: "LocationV2");
        }
    }
}
