using Microsoft.EntityFrameworkCore.Migrations;

namespace MiAsistenteOnline.Web.Migrations
{
    public partial class database4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PedidosDetalles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PedidosDetalles");
        }
    }
}
