using PGB.Pages;
using PGB.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class MenuViewModel : BaseViewModel
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

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                if (model != value)
                    model = value;
                OnPropertyChanged("Model");
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

        public ICommand LogOut { protected set; get; }

        public void SetModel(string modelString)
        {
            if (modelString.Equals("Electric Vehicle"))
            {
                Model = modelString;
            }
            else
            {
                var parse = modelString.Split(';');

                if (parse[0] != null && parse[1] != null)
                {
                    Model = parse[0] + " " + parse[1];
                }
                else
                {
                    Model = modelString;
                }
            }
        }

        public MenuViewModel()
        {
            Username = Settings.Settings.Username;
            Password = Settings.Settings.Password;

            SetModel(Settings.Settings.Model);

            ImagePath = "https://www.codeproject.com/KB/GDI-plus/ImageProcessing2/flip.jpg";
            LogOut = new Command(async() =>
            {
                var response = await _apiServices.LogoutAsync(Settings.Settings.AccessToken);
                Settings.Settings.Model = "Electric Vehicle";
                Settings.Settings.TypeCode = "";
                Settings.Settings.FavRegistrationNumber = "";
                Application.Current.MainPage = new LogIn();
            });
        }
    }
}
