using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class ProductPresentacion : IEntity
    {
        public int Id { get; set; }

        //public int ProductoId { get; set; }
        
        public String Presentacion { get; set; }

        public double Precio { get; set; }

        public Boolean Disponible { get; set; }

        public double Stock { get; set; }


        public Product Producto { get; set; }

    }
}
