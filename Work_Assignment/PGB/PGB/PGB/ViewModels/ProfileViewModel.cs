using PGB.Models;
using PGB.Pages;
using PGB.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class ProfileViewModel : BaseViewModel
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
                OnPropertyChanged("Name");
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

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                    imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        public ICommand Delete { protected set; get; }

        public async void BuildUser()
        {
            User u = await ApiServices.GetUserAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            Username = u.Username;
            Name = u.Name;
            Email = u.Email;
            Nif = u.Nif;
        }
        public ProfileViewModel()
        {
            BuildUser();
            ImagePath = "https://www.codeproject.com/KB/GDI-plus/ImageProcessing2/flip.jpg";


            Delete = new Command(async () =>
            {
                string result = null;
                result = await Application.Current.MainPage.DisplayPromptAsync("Confirmation", "Enter your password to confirm operation.");
                if (result != null)
                {
                    if (!result.Equals(Settings.Settings.Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Wrong Password", "OK");
                    }
                    else
                    {
                        await ApiServices.DeleteUserAsync(Settings.Settings.Username, Settings.Settings.AccessToken);

                        Settings.Settings.Password = string.Empty;
                        Settings.Settings.Username = string.Empty;
                        Settings.Settings.AccessToken = string.Empty;
                        Settings.Settings.TypeCode = string.Empty;
                        Settings.Settings.FavRegistrationNumber = string.Empty;
                        Settings.Settings.Model = "Electric Vehicle";

                        Application.Current.MainPage = new LogIn();
                    }
                }
            });
        }
    }
}
