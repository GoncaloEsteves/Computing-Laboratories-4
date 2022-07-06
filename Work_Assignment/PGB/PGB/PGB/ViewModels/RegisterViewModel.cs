using PGB.Models;
using PGB.Pages;
using PGB.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                    username = value;
                OnPropertyChanged("Username");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                    password = value;
                OnPropertyChanged("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                    name = value;
                OnPropertyChanged("Name");
            }
        }

        private string nif;
        public string Nif
        {
            get { return nif; }
            set
            {
                if (nif != value)
                    nif = value;
                OnPropertyChanged("Nif");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                    email = value;
                OnPropertyChanged("Email");
            }
        }

        public ICommand Register
        {
            get
            {
                return new Command(async () =>
                {
                    if (Username == null || Password == null || Name == null || Email == null || Nif == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Insert all credentials", "OK");
                    }
                    else
                    {
                        User user = new User(Name, Username, Nif, Password, Email);
                        var isRegistered = await _apiServices.RegisterUserAsync(user);
                        if (isRegistered)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success", "User registered with success", "OK");
                            Application.Current.MainPage = new LogIn();
                        } else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Invalid Parameters", "OK");
                        }
                    }
                });
            }
        }

        public RegisterViewModel()
        {
        }

    }
}
