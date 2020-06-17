using Microsoft.EntityFrameworkCore.Migrations;

namespace MiAsistenteOnline.Web.Migrations
{
    public partial class db10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PedidosDetalles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PedidosDetalles",
                nullable: false,
                defaultValue: 0);
        }
    }
}
