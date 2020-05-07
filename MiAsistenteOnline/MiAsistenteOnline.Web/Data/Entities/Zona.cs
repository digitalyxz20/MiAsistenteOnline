using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class Zona : IEntity
    {
        public int Id { get; set; }

        public string Direccion { get; set; }

       //public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
