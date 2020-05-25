using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiAsistenteOnline.Web.Models;


namespace MiAsistenteOnline.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidosDetalles { get; set; }
        public DbSet<ProductPresentacion> ProductPresentaciones { get; set; }
        public DbSet<Zona> Zonas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PedidoDetalle>().HasKey(x => new { x.PedidoId, x.ProductId });
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }



    }
}
