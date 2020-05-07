using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiAsistenteOnline.Web.Data
{
    public class PedidoDetalleRepository : GenericRepository<PedidoDetalle>, IPedidoDetalleRepository
    {
        private readonly DataContext context;

        public PedidoDetalleRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
