using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PGB.Models;
using PGB.Services;
using PGB.Pages;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace PGB.ViewModels
{
    public class RegisterVehicleViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public ObservableCollection<VehicleModels> Models { get; set; }
        VehicleModels selectedModel;
        public VehicleModels SelectedModel
        {
            get { return selectedModel; }
            set
            {
                if (selectedModel != value)
                {
                    selectedModel = value;
                    OnPropertyChanged("SelectedModel");
                }
            }
        }

        private string registration_number;
        public string Registration_number
        {
            get { return registration_number; }
            set
            {
                if (registration_number != value)
                    registration_number = value;
                OnPropertyChanged("Registration_number");
            }
        }

        private string last_consumption;
        public string Last_Consumption
        {
            get { return last_consumption; }
            set
            {
                if (last_consumption != value)
                    last_consumption = value;
                OnPropertyChanged("Last_Consumption");
            }
        }

        public static Regex registrationRegex = new Regex(@"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$");

        public ICommand RegisterVehicle
        {
            get
            {
                return new Command(async () =>
                {
                    if (Registration_number == null || SelectedModel == null || Last_Consumption == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Insert all credentials", "OK");
                    }
                    else
                    {
                        if (!registrationRegex.IsMatch(Registration_number))
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Invalid Registration Number", "OK");
                        }
                        else {
                            //call register in api and handle errors 
                            Vehicle vehicle = new Vehicle(Registration_number, SelectedModel.TypeCode, SelectedModel.Name, Double.Parse(Last_Consumption));
                            var isRegistered = await ApiServices.RegisterVehicleAsync(Settings.Settings.Username, vehicle, Settings.Settings.AccessToken);
                            if (isRegistered)
                            {
                                await Application.Current.MainPage.DisplayAlert("Success", "Vehicle registered with success", "OK");
                                Root root = (Root)Application.Current.MainPage;
                                root.SetDetail("MyVehicles");
                                root.Hide();
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", "Invalid Parameters", "OK");
                            }
                        }
                    }
                });
            }
        }

        public async void GetModels()
        {
            List<VehicleModels> Modelslist =  await ApiServices.GetVehiclesModelsAsync(Settings.Settings.AccessToken);
            
            Modelslist.Sort(
                delegate (VehicleModels p1, VehicleModels p2)
                {
                return p1.Name.CompareTo(p2.Name);
                }
            );

            foreach (VehicleModels v in Modelslist)
            {
                Models.Add(v);
            }
        }

        public RegisterVehicleViewModel()
        {   
            Models = new ObservableCollection<VehicleModels>();
            GetModels();
        }

    }
}
