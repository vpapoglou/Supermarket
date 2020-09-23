using Microsoft.EntityFrameworkCore.Migrations;

namespace SupermarketSklavenitis.Data.Migrations
{
    public partial class changeSpicynessToSweeteners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spicyness",
                table: "MenuItem");

            migrationBuilder.AddColumn<string>(
                name: "Sweeteners",
                table: "MenuItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sweeteners",
                table: "MenuItem");

            migrationBuilder.AddColumn<string>(
                name: "Spicyness",
                table: "MenuItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
