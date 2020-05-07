using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiAsistenteOnline.Web.Data
{
    public class ProductPresentacionRepository : GenericRepository<ProductPresentacion>, IProductPresentacionRepository
    {
        private readonly DataContext context;

        public ProductPresentacionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
