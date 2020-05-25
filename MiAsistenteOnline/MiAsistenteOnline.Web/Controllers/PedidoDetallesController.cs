using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiAsistenteOnline.Web.Data;
using MiAsistenteOnline.Web.Data.Entities;

namespace MiAsistenteOnline.Web.Controllers
{
    public class PedidoDetallesController : Controller
    {
        private readonly DataContext _context;

        public PedidoDetallesController(DataContext context)
        {
            _context = context;
        }

        // GET: PedidoDetalles
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PedidosDetalles.Include(p => p.Pedido).Include(p => p.Product);
            return View(await dataContext.ToListAsync());
        }

        // GET: PedidoDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidosDetalles
                .Include(p => p.Pedido)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["ProductPresentacionId"] = new SelectList(_context.ProductPresentaciones, "Id", "Id");
            return View();
        }

        // POST: PedidoDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,ProductPresentacionId,Cantidad,Subtotal,Id")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoDetalle.PedidoId);
            ViewData["ProductPresentacionId"] = new SelectList(_context.ProductPresentaciones, "Id", "Id", pedidoDetalle.ProductId);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidosDetalles.FindAsync(id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoDetalle.PedidoId);
            ViewData["ProductPresentacionId"] = new SelectList(_context.ProductPresentaciones, "Id", "Id", pedidoDetalle.ProductId);
            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,ProductPresentacionId,Cantidad,Subtotal,Id")] PedidoDetalle pedidoDetalle)
        {
            if (id != pedidoDetalle.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoDetalleExists(pedidoDetalle.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", pedidoDetalle.PedidoId);
            ViewData["ProductPresentacionId"] = new SelectList(_context.ProductPresentaciones, "Id", "Id", pedidoDetalle.ProductId);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidosDetalles
                .Include(p => p.Pedido)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoDetalle = await _context.PedidosDetalles.FindAsync(id);
            _context.PedidosDetalles.Remove(pedidoDetalle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoDetalleExists(int id)
        {
            return _context.PedidosDetalles.Any(e => e.PedidoId == id);
        }
    }
}
