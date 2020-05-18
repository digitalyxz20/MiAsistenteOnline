using GalaSoft.MvvmLight.Command;
using MAO.UIForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MAO.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "javier";
            this.Password = "123";
        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "necesita ingresar el email", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "necesita ingresar el paswprd", "Accept");
                return;
            }

            if (!this.Email.Equals("javier") || !this.Password.Equals("123"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email o password incorrectos", "Accept");
                return;
            }

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

        }
    }
}
