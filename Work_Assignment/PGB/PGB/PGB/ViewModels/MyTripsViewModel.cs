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
    public class MyTripsViewModel : BaseViewModel
    {

        private readonly ApiServices _apiServices = new ApiServices();

        private ObservableCollection<Trip> trips;
        public ObservableCollection<Trip> Trips
        {
            get { return trips; }
            set
            {
                if (trips != value)
                    trips = value;
                OnPropertyChanged("Trips");
            }
        }

        public ICommand Delete { protected set; get; }

        public ICommand Detailed { protected set; get; }

        private async void BuildList()
        {
            /*Position pos = new Position(0, 0);
            List<string> used = new List<string>();
            used.Add("213213");
            Trip tript = new Trip(1, "teste", "AA-00-AA", pos, pos, "2 horas", 4, used, DateTime.Now);
            await _apiServices.AddTripAsync(Settings.Settings.Username, tript, Settings.Settings.AccessToken);*/
            List<Trip> aux = await _apiServices.GetTripsAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            foreach (Trip trip in aux)
            {
                Trips.Add(trip);
            }
        }

        public MyTripsViewModel()
        {

            Trips = new ObservableCollection<Trip>();

            BuildList();

            Delete = new Command<Trip>(async (trip) =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete trip named " + trip.TripName + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    Trips.Remove(trip);
                    await _apiServices.DeleteTripAsync(Settings.Settings.Username, trip.TripId, Settings.Settings.AccessToken);

                }
            });

            Detailed = new Command<Trip>(async (trip) =>
            {
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("Trip", trip);
                root.Hide();
            });
        }
    }
}

