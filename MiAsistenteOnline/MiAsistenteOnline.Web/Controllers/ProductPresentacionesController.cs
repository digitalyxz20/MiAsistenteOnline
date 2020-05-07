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
    public class ProductPresentacionesController : Controller
    {
        private readonly DataContext _context;

        public ProductPresentacionesController(DataContext context)
        {
            _context = context;
        }

        // GET: ProductPresentaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductPresentaciones.ToListAsync());
        }

        // GET: ProductPresentaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPresentacion = await _context.ProductPresentaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPresentacion == null)
            {
                return NotFound();
            }

            return View(productPresentacion);
        }

        // GET: ProductPresentaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductPresentaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Presentacion,Precio,Disponible,Stock")] ProductPresentacion productPresentacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPresentacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productPresentacion);
        }

        // GET: ProductPresentaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPresentacion = await _context.ProductPresentaciones.FindAsync(id);
            if (productPresentacion == null)
            {
                return NotFound();
            }
            return View(productPresentacion);
        }

        // POST: ProductPresentaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Presentacion,Precio,Disponible,Stock")] ProductPresentacion productPresentacion)
        {
            if (id != productPresentacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPresentacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPresentacionExists(productPresentacion.Id))
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
            return View(productPresentacion);
        }

        // GET: ProductPresentaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPresentacion = await _context.ProductPresentaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPresentacion == null)
            {
                return NotFound();
            }

            return View(productPresentacion);
        }

        // POST: ProductPresentaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPresentacion = await _context.ProductPresentaciones.FindAsync(id);
            _context.ProductPresentaciones.Remove(productPresentacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPresentacionExists(int id)
        {
            return _context.ProductPresentaciones.Any(e => e.Id == id);
        }
    }
}
