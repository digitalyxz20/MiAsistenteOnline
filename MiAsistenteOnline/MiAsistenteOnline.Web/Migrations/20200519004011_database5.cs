using Microsoft.EntityFrameworkCore.Migrations;

namespace MiAsistenteOnline.Web.Migrations
{
    public partial class database5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalles_ProductPresentaciones_ProductPresentacionId",
                table: "PedidosDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductPresentacionId",
                table: "PedidosDetalles",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosDetalles_ProductPresentacionId",
                table: "PedidosDetalles",
                newName: "IX_PedidosDetalles_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalles_Products_ProductId",
                table: "PedidosDetalles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalles_Products_ProductId",
                table: "PedidosDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PedidosDetalles",
                newName: "ProductPresentacionId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosDetalles_ProductId",
                table: "PedidosDetalles",
                newName: "IX_PedidosDetalles_ProductPresentacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalles_ProductPresentaciones_ProductPresentacionId",
                table: "PedidosDetalles",
                column: "ProductPresentacionId",
                principalTable: "ProductPresentaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
