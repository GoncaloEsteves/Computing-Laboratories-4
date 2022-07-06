using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PGB.Models;
using PGB.ViewModels;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Root : MasterDetailPage
    {
        private Menu menupage;
        private Page currPage;
        private Page map;
        public Root()
        {
            InitializeComponent();
            menupage = new Menu();
            Master = menupage;
            var mapa = new Map();
            currPage = mapa;
            map = mapa;
            Detail = new NavigationPage(mapa);
            MasterBehavior = MasterBehavior.Popover;
        }

        public void SetDetail(String key)
        { 
            Page value; 
            
            switch (key)
            {
                case "Map": value = map;
                    Detail = new NavigationPage(value);
                    break;
                case "RegisterChargingStation": value = new RegisterChargingStation();
                    Detail = new NavigationPage(value);
                    break;
                case "MyVehicles":
                    value = new MyVehicles();
                    Detail = new NavigationPage(value);
                    break;
                case "PlanTrip":
                    value = new PlanTrip();
                    Detail = new NavigationPage(value);
                    break;
                case "FavoriteStations":
                    value = new FavoriteStations();
                    Detail = new NavigationPage(value);
                    break;
                case "MyTrips":
                    value = new MyTrips();
                    Detail = new NavigationPage(value);
                    break;
                case "Notifications":
                    value = new Notifications();
                    Detail = new NavigationPage(value);
                    break;
                case "RegisterVehicle":
                    value = new RegisterVehicle();
                    Detail = new NavigationPage(value);
                    break;
                case "PlanTripSettings":
                    value = new PlanTripSettings();
                    Detail = new NavigationPage(value);
                    break;
                case "Profile":
                    value = new Profile();
                    Detail = new NavigationPage(value);
                    break;
                case "EditProfile":
                    value = new EditProfile();
                    Detail = new NavigationPage(value);
                    break;
                case "CreditCards":
                    value = new CreditCardList();
                    Detail = new NavigationPage(value);
                    break;
                case "AddCreditCard":
                    value = new AddCreditCard();
                    Detail = new NavigationPage(value);
                    break;
            }
        }

        public void SetDetail(String key, Object info) {
            Page value;

            switch (key)
            {
                case "Vehicle":
                    value = new DetailedVehicle((Vehicle) info);
                    Detail = new NavigationPage(value);
                    break;
                case "ChargingStation":
                    value = new DetailedChargingStation((ExternalChargingStationOCM) info);
                    Detail = new NavigationPage(value);
                    break;
                case "Trip":
                    value = new DetailedTrip((Trip)info);
                    Detail = new NavigationPage(value);
                    break;
                case "SearchStations":
                    value = new SearchStation((IList<ExternalChargingStationOCM>)info);
                    Detail = new NavigationPage(value);
                    break;
            }
        }
        public void Show()
        {
            IsPresented = true;
        }

        public void Hide()
        {
            IsPresented = false;
        }

        public void MainMapShowUserLocation()
        {
            var m = (Map)map;
            m.ShowUserLocation();
        }


        public void SetNewModelFav(string model)
        {
            Menu menu = (Menu)menupage;
            MenuViewModel menuvm = (MenuViewModel)menupage.BindingContext;
            menuvm.SetModel(model);
        }
        public IList<ExternalChargingStationOCM> GetChargingStations()
        {
            var m = (Map)map;
            return m.GetChargingStations();
        }
    }
}