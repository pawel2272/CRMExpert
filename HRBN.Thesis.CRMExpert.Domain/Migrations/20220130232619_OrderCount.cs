using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Migrations
{
    public partial class OrderCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Count",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Orders");
        }
    }
}
