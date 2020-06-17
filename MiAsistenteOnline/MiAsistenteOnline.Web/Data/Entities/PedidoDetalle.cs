using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class PedidoDetalle
    {
        public int PedidoId { get; set; }
        public int ProductId { get; set; }

        public Pedido Pedido { get; set; }
        public Product Product { get; set; }

        public int Cantidad  { get; set; }
        public double Subtotal { get; set; }

    }
}
