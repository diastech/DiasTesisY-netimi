using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiasDataAccessLayer.Migrations.DiasFacilityManagement.SqlServer.Development
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                schema: "lst",
                table: "TicketPriority");

            migrationBuilder.AlterColumn<string>(
                name: "TurkishRepublicIdNumber",
                schema: "idn",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TurkishRepublicIdNumber",
                schema: "idn",
                table: "User",
                type: "nvarchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test",
                schema: "lst",
                table: "TicketPriority",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
