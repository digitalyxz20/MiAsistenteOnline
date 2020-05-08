using MiAsistenteOnline.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> GetAllWithUsers();

        IQueryable<Product> GetAllCategory();

        IQueryable<Product> GetProductoPorCategoria(string categoria);

    }

}
