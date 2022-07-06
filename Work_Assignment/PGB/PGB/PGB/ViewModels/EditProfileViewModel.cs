using PGB.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net;
using PGB.Pages;

namespace PGB.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

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

        private string repeat_password;
        public string Repeat_Password
        {
            get { return repeat_password; }
            set
            {
                if (repeat_password != value)
                    repeat_password = value;
                OnPropertyChanged("Repeat_Password");
            }
        }
        private string new_password;
        public string New_Password
        {
            get { return new_password; }
            set
            {
                if (new_password != value)
                    new_password = value;
                OnPropertyChanged("New_Password");
            }
        }

        private bool saveNewPassword;
        public bool SaveNewPassword
        {
            get { return saveNewPassword; }
            set
            {
                if (saveNewPassword != value)
                    saveNewPassword = value;
                OnPropertyChanged("SaveNewPassword");
            }
        }

        public ICommand ChangePassword { protected set; get; }

        public EditProfileViewModel()
        {
            ChangePassword = new Command(async () =>
            {
                if (Password == null || Repeat_Password == null || New_Password == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "All entries must be filled", "OK");
                }
                else
                {
                    if (!New_Password.Equals(Repeat_Password) || New_Password.Equals(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Passwords don't match or trying to change to the same password.", "OK");
                    }
                    else
                    {
                        if (!Password.Equals(Settings.Settings.Password))
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Wrong Password.", "OK");
                        }
                        else
                        {
                            var choice = await Application.Current.MainPage.DisplayAlert("Confirmation", "Changing your password will make you logout so you can login back again. Are you sure that you want to change your password?", "Ok", "Cancel");
                            if (choice)
                            {
                                var response = await _apiServices.ChangePasswordAsync(Settings.Settings.Username, Password, New_Password, Settings.Settings.AccessToken);
                                switch (response)
                                {
                                    case HttpStatusCode.OK:
                                        if (SaveNewPassword)
                                        {
                                            Settings.Settings.Password = New_Password;
                                        } else
                                        {
                                            Settings.Settings.Password = "";
                                        }
                                        await Application.Current.MainPage.DisplayAlert("Success", "Password was changed with success.", "OK");
                                        Settings.Settings.Model = "Electric Vehicle";
                                        Settings.Settings.TypeCode = "";
                                        Application.Current.MainPage = new LogIn();
                                        break;
                                    case HttpStatusCode.BadRequest:
                                        await Application.Current.MainPage.DisplayAlert("Alert", "Invalid username.", "OK");
                                        break;
                                    case HttpStatusCode.NotFound:
                                        await Application.Current.MainPage.DisplayAlert("Alert", "User not found.", "OK");
                                        break;
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
