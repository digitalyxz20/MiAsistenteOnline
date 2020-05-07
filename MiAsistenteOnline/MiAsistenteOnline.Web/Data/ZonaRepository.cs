using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiAsistenteOnline.Web.Data
{
    public class ZonaRepository : GenericRepository<Zona>, IZonaRepository
    {
        private readonly DataContext context;

        public ZonaRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
