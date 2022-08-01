using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class UpdateTicketAddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserResolutionTime",
                schema: "usr",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UserResponseTime",
                schema: "usr",
                table: "Ticket");
        }
    }
}
