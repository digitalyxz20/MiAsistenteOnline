using MiAsistenteOnline.Web.Data;
using MiAsistenteOnline.Web.Data.Entities;
using MiAsistenteOnline.Web.Helpers;
using MiAsistenteOnline.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IClienteRepository clienteRepository;

        public AccountController(IUserHelper userHelper, IClienteRepository clienteRepository)
        {
            this.userHelper = userHelper;
            this.clienteRepository = clienteRepository;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Categoria","Products");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Categoria", "Products");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                var cliente = await this.clienteRepository.ObtenerClientePorDni(model.Username);
                if (user == null && cliente == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = $"{model.Username}@hot.com",
                        UserName = model.Username,
                        PhoneNumber = model.Celular

                    };

                    cliente = new Cliente
                    {
                        Nombres = model.FirstName,
                        DNI = model.Username,
                        Email = $"{model.Username}@hot.com",
                        Celular = model.Celular
                    };






                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    await this.clienteRepository.CreateAsync(cliente);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "El Cliente no puede ser creado.");
                        return this.View(model);
                    }


                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    var result2 = await this.userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Categoria", "Products");
                    }

                    this.ModelState.AddModelError(string.Empty, "No se pudo iniciar sesion. Intentelo de nuevo");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "El Cliente ya ha sido registrado.");
            }

            return this.View(model);
        }




    }
}
