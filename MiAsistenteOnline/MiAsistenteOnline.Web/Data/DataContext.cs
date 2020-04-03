using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MiAsistenteOnline.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Product> Products { get; set; } 



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
    }
}
