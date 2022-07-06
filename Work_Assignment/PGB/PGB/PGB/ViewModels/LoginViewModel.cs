using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PGB.Settings;
using PGB.Pages;
using PGB.Services;
using System.Net;

namespace PGB.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
        public ICommand Login
        {
            get
            {
                return new Command(async () =>
                {
                    if (Username.Equals("") && Password.Equals(""))
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Insert an username and password", "OK");
                    }
                    else
                    {
                        if (Username.Equals(""))
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Insert an username", "OK");
                        }

                        if (Password.Equals(""))
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Insert a password", "OK");
                        }
                    }

                    if (!Username.Equals("") && !Password.Equals(""))
                    {
                        //call log_in in api and if are error codes display them
                        var statusCode = await ApiServices.LoginAsync(Username, Password);
                        if (statusCode == HttpStatusCode.BadRequest)
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Invalid username or password.", "OK");
                        } else
                        {
                            if(statusCode == HttpStatusCode.OK)
                            {
                                Settings.Settings.Username = Username;
                                Settings.Settings.Password = Password;
                                Application.Current.MainPage = new Root();
                            }
                        }
                    }
                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Settings.Username;
            Password = Settings.Settings.Password;
        }
    }
}
