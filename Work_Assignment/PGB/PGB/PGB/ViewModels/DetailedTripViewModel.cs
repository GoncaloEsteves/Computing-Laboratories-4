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
    public class DetailedTripViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private Trip trip;
        public Trip Trip
        {
            get { return trip; }
            set
            {
                if (trip != value)
                    trip = value;
                OnPropertyChanged("trip");
            }
        }

        public ICommand Delete { protected set; get; }

        public DetailedTripViewModel()
        {

            Delete = new Command(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete trip named " + trip.TripName + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    await _apiServices.DeleteTripAsync(Settings.Settings.Username, trip.TripId, Settings.Settings.AccessToken);


                    Root root = (Root)Application.Current.MainPage;
                    root.SetDetail("MyTrips");
                    root.Hide();
                }
            });
        }
    }
}