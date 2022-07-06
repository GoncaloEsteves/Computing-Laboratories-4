using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class RegisterChargingStationViewModel : BaseViewModel
    {
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

        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                if (street != value)
                    street = value;
                OnPropertyChanged("Street");
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                    city = value;
                OnPropertyChanged("City");
            }
        }

        private string location_type;
        public string Location_Type
        {
            get { return location_type; }
            set
            {
                if (location_type != value)
                    location_type = value;
                OnPropertyChanged("Location_Type");
            }
        }
        private string access_type;
        public string Access_Type
        {
            get { return access_type; }
            set
            {
                if (access_type != value)
                    access_type = value;
                OnPropertyChanged("Access_Type");
            }
        }

        private string restritions;
        public string Restritions
        {
            get { return restritions; }
            set
            {
                if (restritions != value)
                    restritions = value;
                OnPropertyChanged("Restritions");
            }
        }

        private string additional_info;
        public string Additional_Info
        {
            get { return additional_info; }
            set
            {
                if (additional_info != value)
                    additional_info = value;
                OnPropertyChanged("Additional_Info");
            }
        }

        public ICommand RegisterChargingStation
        {
            get
            {
                return new Command(async () =>
                {
                    if (Name == null || Street == null || City == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Insert all credentials", "OK");
                    }
                    else
                    {
                        //call add station in api and handle errors 
                    }
                });
            }
        }

        public RegisterChargingStationViewModel()
        {
        }

    }
}
