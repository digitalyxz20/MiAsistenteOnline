using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiAsistenteOnline.Web.Data;
using MiAsistenteOnline.Web.Data.Entities;
using MiAsistenteOnline.Web.Helpers;
using MiAsistenteOnline.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MiAsistenteOnline.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;
        private readonly IClienteRepository clienteRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IPedidoDetalleRepository pedidoDetalleRepository;

        public ProductsController(IProductRepository productRepository,
                                  IUserHelper userHelper,
                                  IClienteRepository clienteRepository,
                                  IPedidoRepository pedidoRepository,
                                  IPedidoDetalleRepository pedidoDetalleRepository
                                  )
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
            this.clienteRepository = clienteRepository;
            this.pedidoRepository = pedidoRepository;
            this.pedidoDetalleRepository = pedidoDetalleRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(this.productRepository.GetAll().OrderBy(t => t.Name));
        }


        //mostrar categorias
        public IActionResult Categoria()
        {
            return View(this.productRepository.GetAllCategory().OrderBy(t => t.GrupoArticulo));
        }



        [Route("/Products/BuscarProductos", Name = "buscarProductos")]
        public IActionResult BuscarProductos(string GrupoArticulo)
        {
            return View(this.productRepository.GetProductoPorCategoria(GrupoArticulo).OrderBy(p => p.Name));
        }

        
        public IActionResult BusProductos(string GrupoArticulo)
        {
            return PartialView(this.productRepository.GetProductoPorCategoria(GrupoArticulo).OrderBy(p => p.Name));
        }



        public void SetSession(Object obj)
        {
            HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(obj));
        }

        public string GetSession()
        {
            return HttpContext.Session.GetString("carrito");
        }

        public IActionResult AgregarCarritoSessionIni(int Id, string Detalle, double Precio, double Cantidad)
        {
            List<PedidoDetalle> lista = new List<PedidoDetalle>();
            var value = GetSession();
            if (value == null)
            {

                lista = new List<PedidoDetalle>();
                return PartialView("AgregarCarritoSession", lista);
            }
            lista = JsonConvert.DeserializeObject<List<PedidoDetalle>>(GetSession());
            return PartialView("AgregarCarritoSession",lista);
        }


        [HttpPost]
        public IActionResult AgregarCarritoSession(int Id,string Detalle,double  Precio,double Cantidad )
        {

            var product = new Product() {
                Id = Id,
                Name = Detalle,
                Price = Convert.ToDecimal(Precio)

            };

            var modelo = new PedidoDetalle()
            {
                ProductId = Id,
                Cantidad = Convert.ToInt32(Cantidad),
                Subtotal = Cantidad * Precio,
                Product=product

            };

            if (GetSession() == null)
            {
                List<PedidoDetalle> lista = new List<PedidoDetalle>();
                lista.Add(modelo);
                SetSession(lista);
            }
            else
            {
                List<PedidoDetalle> lista = JsonConvert.DeserializeObject<List<PedidoDetalle>>(GetSession());
                lista.Add(modelo);
                SetSession(lista);
            }

            var value = GetSession();
            List<PedidoDetalle> lista1 = JsonConvert.DeserializeObject<List<PedidoDetalle>>(GetSession());
            return PartialView(lista1);
        }




        public IActionResult VerCarrito()
        {
            List<PedidoDetalle> lista = JsonConvert.DeserializeObject<List<PedidoDetalle>>(GetSession());
            return PartialView(lista);
        }


        [HttpPost]
        public async Task<IActionResult> InsertarVenta()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                ViewData["Message"] = "Usted necesita iniciar su cuenta";
                return View("Account/Login");
            }

            var value = GetSession();
            if (value == null)
            {
                ViewData["Message"] = "No tiene productos en el carrito.";
                return PartialView();
            }

            var usuario = await this.userHelper.GetUserByEmailAsync($"{ this.User.Identity.Name}@hot.com");
            List<PedidoDetalle> lista = JsonConvert.DeserializeObject<List<PedidoDetalle>>(GetSession());
            var cliente = await this.clienteRepository.ObtenerClientePorDni(this.User.Identity.Name);

            var total = 0.0; 
            foreach (var i in lista)
            {
                total += i.Subtotal;
            }

            var pedido = new Pedido
            {
                Cliente = cliente,
                User = usuario,
                FechaPedido = DateTime.Now,
                Entregado = false,
                Total = total
            };

            try
            {
                pedido.Id = await this.pedidoRepository.CreateAsync(pedido);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Hubo Un error en ingresar el Pedido.";
                return PartialView();
            }

            try
            {
                foreach (var i in lista)
                {
                    i.PedidoId = pedido.Id;
                    await this.pedidoDetalleRepository.CreateAsync(i);
                }
            }
            catch (Exception)
            {
                await this.pedidoRepository.DeleteAsync(pedido);
                ViewData["Message"] = "Hubo Un error en ingresar los productos del pedido";
                return PartialView();
            }

            ViewData["Message"] = "Se registro con exito, a continuacion no comunicaremos con usted via telefonica . Gracias";
            return PartialView();

        }








        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Products",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{file}";
                }

                var product = this.ToProduct(view, path);

                product.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                await this.productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                Id = view.Id,
                ImageUrl = path,
                IsAvailabe = view.IsAvailabe,
                LastPurchase = view.LastPurchase,
                LastSale = view.LastSale,
                Name = view.Name,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User
            };
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProductViewModel(product);
            return View(view);
        }

        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailabe = product.IsAvailabe,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel view)
        {

            if (ModelState.IsValid)
            {


                try
                {
                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";
                        path = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Products", file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Products/{file}";
                    }

                    var product = this.ToProduct(view, path);
                    await this.productRepository.UpdateAsync(product);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await this.productRepository.ExistAsync(view.Id))
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
            return View(view);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.productRepository.GetByIdAsync(id);
            await this.productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


    }
}
