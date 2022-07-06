using PGB.ViewModels;
using System;
using PGB.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedChargingStation : ContentPage
    {
        public DetailedChargingStation(ExternalChargingStationOCM station)
        {
            InitializeComponent();

            var detailedchargingstationViewModel = new DetailedChargingStationViewModel(MyMap, station);

            BindingContext = detailedchargingstationViewModel;
        }

        void GoToMainMap(object sender, EventArgs args)
        {
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("Map");
            root.Hide();
        }

        void GoToFavs(object sender, EventArgs args)
        {
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("FavoriteStations");
            root.Hide();
        }
    }
}