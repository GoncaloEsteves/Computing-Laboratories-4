using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using System.Reflection;
using PGB.Data;
using PGB.Models;
using PGB.Services;
using Position = Xamarin.Forms.GoogleMaps.Position;
using Distance = Xamarin.Forms.GoogleMaps.Distance;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using PGB.ViewModels;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class Map : ContentPage, INotifyPropertyChanged
    {
        private bool _isLoading;

        private IList<ExternalChargingStationOCM> _chargingStations;
        public Map()
        {
            InitializeComponent();
            IsLoading = false;
            _chargingStations = new List<ExternalChargingStationOCM>();
            UpdateMap();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void ShowUserLocation()
        {
            MyMap.MyLocationEnabled = true;
            MyMap.UiSettings.MyLocationButtonEnabled = true;
        }

        private async void UpdateMap()
        {

            try
            {
                // Activate loading Icon
                IsLoading = true;
                AddMapStyle();

                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(41.5607359, -8.3984255), Distance.FromKilometers(300)));
                }
                else
                {
                    ShowUserLocation();
                    var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();

                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc.Latitude, loc.Longitude), Distance.FromKilometers(300)));

                }
                // In case the database is empty
                var size = App.Database.DatabaseSize();
                
                if (App.Database.DatabaseSize() != 834)
                {
                    await LoadChargingStations();
                }
                else
                {
                    // Get the charging stations from the local database
                    _chargingStations = await App.Database.GetChargingStationsAsync();
                }
                /*Console.WriteLine("AddToMap\n");*/
                foreach (var station in _chargingStations)
                {
                    
                    AddMapPin(station);
                }
                await App.Database.SaveChargingStationsAsync(_chargingStations);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsLoading = false;

        }

        private void PinOnClicked(object sender, EventArgs e)
        {
            var selectedPin = sender as Pin;
            ExternalChargingStationOCM detailed = null;
            
             if (App.Database.DatabaseSize() != _chargingStations.Count)
             {
                 foreach (ExternalChargingStationOCM externalChargingStation in _chargingStations)
                 { 
                     if (externalChargingStation.Id == (long) selectedPin.Tag)
                     {
                         detailed = externalChargingStation;
                         break;
                     }
                 }
             }
             else
             {
                 detailed = App.Database.GetChargingStationAsync((long)selectedPin.Tag).Result;   
             }
 
            var root = (Root)Application.Current.MainPage;
            root.SetDetail("ChargingStation", detailed);
            root.Hide();
        }

        private async Task LoadChargingStations()
        {
            OpenChargeMapApiService openChargeMapApi = new OpenChargeMapApiService();

            // Load the charging stations from the API and store them at the local database
            _chargingStations = await openChargeMapApi.LoadOCMChargingStations();
            Log.Warning(Constants.Tag, "LoadChargingStationsAtMap");
            
        }

        void AddMapPin(ExternalChargingStationOCM station)
        {   
            if(station.OperatorInfo == null || station.OperatorInfo.Title == null)
            {
                if(station.OperatorInfo == null) { station.OperatorInfo = new OperatorInfo(); }
                station.OperatorInfo.Title = "(Unknown Operator)";
            }
            if (station.AddressInfo != null && station.AddressInfo.Title != null)//&& station.OperatorInfo != null && station.OperatorInfo.Title != null
            {
                var pin = new Pin
                {
                    Position = new Position(station.AddressInfo.Latitude, station.AddressInfo.Longitude),
                    Label = station.AddressInfo.Title,
                    Tag = station.Id
                };
                if(station.OperatorInfo != null && station.OperatorInfo.Title != null)
                {
                    pin.Address = station.OperatorInfo.Title;
                }
                try
                {
                    if (station.Connections[0].Level.IsFastChargeCapable)
                    {
                        pin.Icon = BitmapDescriptorFactory.FromBundle("fast_charger_3.png");
                    }
                    else
                    {
                        pin.Icon = BitmapDescriptorFactory.FromBundle("normal_charger_3.png");
                    }
                }
                catch (Exception)
                {
                    pin.Icon = BitmapDescriptorFactory.FromBundle("normal_charger_3.png");
                }

                pin.Clicked += PinOnClicked;
                
                MyMap.Pins.Add(pin);
            }
        }

        void AddMapStyle()
        {
            var assembly = typeof(PlanTrip).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"PGB.Styles.MapStyle.json");
            string styleFile;
            using (var reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            MyMap.MapStyle = MapStyle.FromJson(styleFile);
        }
        
        public bool IsLoading
        {
            get
            {
                return this._isLoading;
            }
            set
            {
                this._isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        async void GoToMainMap(Object sender, EventArgs e)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status == PermissionStatus.Granted)
                {
                    ShowUserLocation();
                }
            }
            else
            {
                if (_chargingStations.Count > 0)
                {
                    var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                    ExternalChargingStationOCM closestStation = _chargingStations.ElementAt(0);
                    double minDistance = -1;
                    double aux;
                    foreach (ExternalChargingStationOCM c in _chargingStations)
                    {
                        if (c.AddressInfo != null && c.AddressInfo.Title != null)
                        {
                            aux = loc.CalculateDistance(new Location(c.AddressInfo.Latitude, c.AddressInfo.Longitude), DistanceUnits.Kilometers);
                            if (aux < minDistance || minDistance < 0)
                            {
                                closestStation = c;
                                minDistance = aux;
                            }
                        }
                    }

                    var location = new Location(closestStation.AddressInfo.Latitude, closestStation.AddressInfo.Longitude);
                    var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

                    await Xamarin.Essentials.Map.OpenAsync(location, options);
                }
            }
        }

        async void GoToFavorites(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoriteStations());
        }

        async void GoToMyTrips(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTrips());
        }

        async void GoToUserProfile(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        public IList<ExternalChargingStationOCM> GetChargingStations()
        {
            return _chargingStations;
        }

        private void OnEnterAddressTapped(object sender, EventArgs e)
        {
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("SearchStations", _chargingStations);
                root.Hide();
        }
    }
}
