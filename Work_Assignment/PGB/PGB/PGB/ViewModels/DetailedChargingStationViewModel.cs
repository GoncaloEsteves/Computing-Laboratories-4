using PGB.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using PGB.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Xamarin.Forms.GoogleMaps.Position;
using Xamarin.Essentials;
using PGB.Pages;

namespace PGB.ViewModels
{
    public class DetailedChargingStationViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private ExternalChargingStationOCM _chargingStation;
        public ExternalChargingStationOCM ChargingStation
        {
            get => _chargingStation;
            set
            {
                if (value != null && _chargingStation != value)
                    _chargingStation = value;
                OnPropertyChanged("ChargingStation");
            }
        }

        private bool _fav;
        public bool Fav
        {
            get { return _fav; }
            set
            {
                if (!_fav.Equals(value))
                    _fav = value;
                OnPropertyChanged("Fav");
            }
        }

        public Xamarin.Forms.GoogleMaps.Map Map { get; private set; }

        private int _numChargers;
        public int NumChargers
        {
            get { return _numChargers; }
            set
            {
                if (_numChargers != value)
                    _numChargers = value;
                OnPropertyChanged("NumChargers");
            }
        }

        public async void IsFavorite()
        {
            List<string> favs = await ApiServices.GetFavoritesAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            IList<long> favsLong = new List<long>();
            foreach (string s in favs)
            {
                favsLong.Add(Convert.ToInt64(s));
            }
            Fav = favsLong.Contains(ChargingStation.Id);
        }

        public DetailedChargingStationViewModel(Xamarin.Forms.GoogleMaps.Map map, ExternalChargingStationOCM chargingstation)
        {
            Map = map;
            NumChargers = 0;
            ChargingStation = chargingstation;
            foreach(Connection connect in chargingstation.Connections)
            {
                try
                {
                    if (connect.StatusType.IsOperational)
                    {
                        NumChargers++;
                    }
                }
                catch (Exception) { }
            }
            IsFavorite();
            UpdateMap(chargingstation);
        }

        private async void UpdateMap(ExternalChargingStationOCM station)
        {

            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                {
                    Map.MyLocationEnabled = true;
                    Map.UiSettings.MyLocationButtonEnabled = true;
                }
                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(station.AddressInfo.Latitude, station.AddressInfo.Longitude), Distance.FromKilometers(1)));

                    if (station.AddressInfo != null && station.AddressInfo.Title != null)//&& station.OperatorInfo != null && station.OperatorInfo.Title != null
                {
                        var pin = new Pin
                        {
                            Position = new Position(station.AddressInfo.Latitude, station.AddressInfo.Longitude),
                            Label = station.AddressInfo.Title
                        };
                    if (station.OperatorInfo != null && station.OperatorInfo.Title != null)
                    {
                        pin.Address = station.OperatorInfo.Title; ;
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

                        Map.PinClicked += PinOnClicked;

                        Map.Pins.Add(pin);
                }
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void PinOnClicked(object sender, EventArgs e)
        {
            var selectedPin = sender as Pin;

        }

        public ICommand AddFavorite
        {
            get
            {
                return new Command(async () =>
                {
                    if (!Fav)
                    {
                        //call register in api and handle errors 
                        var resp = await _apiServices.AddFavoriteAsync(Settings.Settings.Username, ChargingStation.Id.ToString(), Settings.Settings.AccessToken);
                        if (resp)
                        {
                            Fav = true;
                            await Application.Current.MainPage.DisplayAlert("Success", "Charging station added to your favorites", "OK");

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Charging station already in your favorites", "OK");
                        }
                    } else
                    {
                        bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete charging stattion " + _chargingStation.AddressInfo.Title + "?", "Yes", "No");
                        if (answer)
                        {
                            //opreate the remove item
                            Fav = false;
                            await ApiServices.DeleteFavoriteAsync(Settings.Settings.Username, _chargingStation.Id.ToString(), Settings.Settings.AccessToken);
                        }
                    }
                });
            }
        }

        public ICommand NavigateTo
        {
            get
            {
                return new Command(async () =>
                {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                    if (status != PermissionStatus.Granted)
                    {
                        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                        status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                        if (status == PermissionStatus.Granted)
                        {
                            Root root = (Root)Application.Current.MainPage;
                            root.MainMapShowUserLocation();
                            Map.MyLocationEnabled = true;
                            Map.UiSettings.MyLocationButtonEnabled = true;
                        }
                    }
                    else
                    {
                        var location = new Location(ChargingStation.AddressInfo.Latitude, ChargingStation.AddressInfo.Longitude);
                        var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

                        await Xamarin.Essentials.Map.OpenAsync(location, options);
                    }
                });
            }
        }
    }
}
