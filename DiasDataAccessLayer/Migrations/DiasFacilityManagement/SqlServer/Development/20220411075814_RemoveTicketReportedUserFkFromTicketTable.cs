using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class RemoveTicketReportedUserFkFromTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_ReportedUserId_User_Id",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_TicketReportedUserId",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TicketReportedUserId",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.AddColumn<string>(
                name: "TicketReportedUserNameSurname",
                schema: "usr",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketReportedUserPhone",
                schema: "usr",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketReportedUserNameSurname",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TicketReportedUserPhone",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "TicketReportedUserId",
                schema: "usr",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketReportedUserId",
                schema: "usr",
                table: "Ticket",
                column: "TicketReportedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ReportedUserId_User_Id",
                schema: "usr",
                table: "Ticket",
                column: "TicketReportedUserId",
                principalSchema: "idn",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
