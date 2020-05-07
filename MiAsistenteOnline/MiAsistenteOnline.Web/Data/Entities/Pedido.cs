using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class Pedido : IEntity
    {
        public int Id { get; set; }

        public Cliente Cliente { get; set; }

        //public int Cliente { get; set; }

        //public int UserId { get; set; }

        public User User { get; set; }

        public Double Total { get; set; }

        public DateTime FechaPedido { get; set; }

        public DateTime FechaEntrega { get; set; }

        public Boolean Entregado { get; set; }

        public string Observaciones { get; set; }

       



    }
}
