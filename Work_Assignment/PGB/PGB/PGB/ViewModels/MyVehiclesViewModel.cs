using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using PGB.Models;
using PGB.Pages;
using PGB.Services;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class MyVehiclesViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private ObservableCollection<Vehicle> vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return vehicles; }
            set
            {
                if (vehicles != value)
                    vehicles = value;
                OnPropertyChanged("Vehicles");
            }
        }

        public ICommand Delete { protected set; get; }

        public ICommand Detailed { protected set; get; }

        public ICommand Favorite { protected set; get; }
        private async void BuildList()
        {
            List<Vehicle> aux = await _apiServices.GetVehiclesAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            foreach (Vehicle vehicle in aux)
            {
                Vehicles.Add(vehicle);
            }
        }

        public MyVehiclesViewModel()
        {

            //Vehicle teste = new Vehicle("teste");
            Vehicles = new ObservableCollection<Vehicle>();
            //Vehicles.Add(teste);
            BuildList();
            Delete = new Command<Vehicle>(async (vehicle) =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete vehicle with registration number " + vehicle.RegistrationNumber + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    Vehicles.Remove(vehicle);
                    await _apiServices.DeleteVehicleAsync(Settings.Settings.Username, vehicle.RegistrationNumber, Settings.Settings.AccessToken);
                    if (Settings.Settings.FavRegistrationNumber.Equals(vehicle.RegistrationNumber))
                    {
                        Settings.Settings.Model = "Electric Vehicle";
                        Settings.Settings.TypeCode = "";
                        Settings.Settings.FavRegistrationNumber = "";
                        Root root = (Root)Application.Current.MainPage;
                        root.SetNewModelFav("Electric Vehicle");

                    }

                }
            });

            Detailed = new Command<Vehicle>(async (vehicle) =>
            {
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("Vehicle", vehicle);
                root.Hide();
            });

            Favorite = new Command<Vehicle>(async (vehicle) =>
            {
                if (!Settings.Settings.FavRegistrationNumber.Equals(vehicle.RegistrationNumber))
                {
                    Settings.Settings.FavRegistrationNumber = vehicle.RegistrationNumber;
                    Settings.Settings.Model = vehicle.Name;
                    Settings.Settings.TypeCode = vehicle.TypeCode;
                    Root root = (Root)Application.Current.MainPage;
                    root.SetNewModelFav(vehicle.Name);

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
