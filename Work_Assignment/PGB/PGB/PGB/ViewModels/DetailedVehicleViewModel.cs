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
    public class DetailedVehicleViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private Vehicle vehicle;
        public Vehicle Vehicle
        {
            get { return vehicle; }
            set
            {
                if (vehicle != value)
                    vehicle = value;
                OnPropertyChanged("Vehicle");
            }
        }

        private bool _fav;
        public bool Fav
        {
            get { return _fav; }
            set
            {
                if (_fav != value)
                    _fav = value;
                OnPropertyChanged("Fav");
            }
        }

        public ICommand Delete { protected set; get; }

        public ICommand Favorite { protected set; get; }

        public DetailedVehicleViewModel()
        {
            Delete = new Command(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete vehicle with registration number " + vehicle.RegistrationNumber + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    await _apiServices.DeleteVehicleAsync(Settings.Settings.Username, Vehicle.RegistrationNumber, Settings.Settings.AccessToken);

                    if (Settings.Settings.FavRegistrationNumber.Equals(vehicle.RegistrationNumber))
                    {
                        Settings.Settings.Model = "Electric Vehicle";
                        Settings.Settings.TypeCode = "";
                        Settings.Settings.FavRegistrationNumber = "";
                        Root rootaux = (Root)Application.Current.MainPage;
                        rootaux.SetNewModelFav("Electric Vehicle");

                    }

                    Root root = (Root)Application.Current.MainPage;
                    root.SetDetail("MyVehicles");
                    root.Hide();
                }
            });

            Favorite = new Command(async () =>
            {
                if (!Settings.Settings.FavRegistrationNumber.Equals(vehicle.RegistrationNumber))
                {
                    Settings.Settings.FavRegistrationNumber = vehicle.RegistrationNumber;
                    Settings.Settings.Model = vehicle.Name;
                    Settings.Settings.TypeCode = vehicle.TypeCode;
                    Root root = (Root)Application.Current.MainPage;
                    root.SetNewModelFav(vehicle.Name);
                    Fav = true;
                    await Application.Current.MainPage.DisplayAlert("Success", "Selected vehicle selected as favorite.", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Selected vehicle is already favorite.", "Ok");
                }
            });
        }
    }
}
