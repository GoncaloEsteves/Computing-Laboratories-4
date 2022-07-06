using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            Title = "Menu";
            InitializeComponent();
        }

        public void GoToProfile(object sender, EventArgs args)
        {
                
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("Profile");
                root.Hide();
        }

        void GoToMap(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Root root = (Root) Application.Current.MainPage;
            root.SetDetail("Map");
            root.Hide();
        }

        async void PlanTrip(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;

            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status == PermissionStatus.Granted)
                {
                    root.MainMapShowUserLocation();
                }
            }
            else
            {
                root.SetDetail("PlanTrip");
                root.Hide();
            }

        }

        void FavoriteStations(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("FavoriteStations");
            root.Hide();

        }

        void MyTrips(object sender, EventArgs args)
        {

            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("MyTrips");
            root.Hide();
        }

        void MyVehicles(object sender, EventArgs args)
        {
           
            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("MyVehicles");
            root.Hide();
        }

        void AddCharger(object sender, EventArgs args)
        {

            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("RegisterChargingStation");
            root.Hide();
        }

        void Notifications(object sender, EventArgs args)
        {

            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("Notifications");
            root.Hide();
        }
    }
}