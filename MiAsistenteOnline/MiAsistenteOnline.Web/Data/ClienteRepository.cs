﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiAsistenteOnline.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiAsistenteOnline.Web.Data
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly DataContext context;

        public ClienteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public IQueryable<Cliente> GetAllWithUsers()
        {
            return context.Clientes.Include(p => p.Zona );
        }

        public async Task<Cliente> ObtenerClientePorDni(string dni)
        {
            return await context.Clientes.FirstAsync(p => p.DNI == dni);
        }

    }
}
