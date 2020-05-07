using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class PedidoDetalle :IEntity
    {
        public int PedidoId { get; set; }
        public int ProductPresentacionId { get; set; }

        public Pedido Pedido { get; set; }
        public ProductPresentacion ProductPresentacion { get; set; }

        public int Cantidad  { get; set; }
        public double Subtotal { get; set; }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
