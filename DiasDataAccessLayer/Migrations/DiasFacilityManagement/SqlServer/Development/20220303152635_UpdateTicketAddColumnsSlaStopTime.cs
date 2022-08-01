using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class UpdateTicketAddColumnsSlaStopTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SlaStopTime",
                schema: "usr",
                table: "Ticket",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlaStopTime",
                schema: "usr",
                table: "Ticket");
        }
    }
}
