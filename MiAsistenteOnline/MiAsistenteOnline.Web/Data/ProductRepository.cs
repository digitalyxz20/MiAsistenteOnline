using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Product> GetAllWithUsers()
        {
            return this.context.Products.Include( p => p.User);
        }


        public IQueryable<Product> GetAllCategory()
        {
            return this.context.Products.GroupBy(x => x.GrupoArticulo).Select(x => x.First());
        }

        public IQueryable<Product> GetProductoPorCategoria(string categoria)
        {
            return this.context.Products.Where(x => x.GrupoArticulo == categoria);
        }
    }
}
