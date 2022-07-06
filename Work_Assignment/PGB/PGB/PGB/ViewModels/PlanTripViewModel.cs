using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PGB.Helpers;
using PGB.Models;
using PGB.Services;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace PGB.ViewModels
{
    public class PlanTripViewModel : INotifyPropertyChanged
    {
        public ICommand CalculateRouteCommand { get; set; }

        public ICommand UpdatePositionCommand { get; set; }

        public ICommand LoadRouteCommand { get; set; }
        
        public ICommand CancelTripCommand { get; set; }

        public ICommand CleanFieldsCommand { get; set; }
        
        public ICommand VerifiyIfHasVehicles { get; set; }
        
        public ICommand FavoriteVehicleSelectedCommand { get; set; }
        
        public ICommand SaveTripCommand { get; set; }
        
        public ICommand OpenTripOnMapCommand { get; set; }

        // API'S
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
        IternioApiServiceInterface iternioApi = new IternioApiService();
        ApiServices pgbApiServices = new ApiServices();

        public bool HasRouteRunning { get; set; }
        public bool HasFavoriteVehicle { get; set; }
        public bool TripSaved { get; set; }
        private string _originLatitude;
        private string _originLongitude;
        private string _destinationLatitude;
        private string _destinationLongitude;
        private string _typecode;
        private string _tripName;
        private int _initialSoc;
        private int _refConsumption;
        private bool _avoidTolls;
        private bool _avoidHighways;
        private bool _avoidFerries;
        private List<Vehicle> _userVehicles;
        private List<string> _userVehiclesRegistrationNumbers;
        private string _selectedVehicle;
        private long _duration;
        private double _cost;
        private List<string> _usedChargingStations;
        private List<IternioStep> _steps;
        private List<TripDetailCell> _tripDetails;
        private string auxOriginName;
        private string auxDestName;

        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected { get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
            }
        }
        public ICommand FocusOriginCommand { get; set; }
        public ICommand GetPlacesCommand { get; set; }
        public ICommand GetPlaceDetailCommand { get; set; }

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();

        public bool ShowRecentPlaces { get; set; }
        bool _isPickupFocused = true;

        string _originText;
        public string OriginText
        {
            get
            {
                return _originText;
            }
            set
            {
                _originText = value;
                if (!string.IsNullOrEmpty(_originText))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_originText);
                }
            }
        }

        string _destinationText;
        public string DestinationText
        {
            get
            {
                return _destinationText;
            }
            set
            {
                _destinationText = value;
                if (!string.IsNullOrEmpty(_destinationText))
                {
                    _isPickupFocused = false;
                    GetPlacesCommand.Execute(_destinationText);
                }
            }
        }

        public bool AvoidTollsToggled
        {
            get => _avoidTolls;
            set => _avoidTolls = value;
        }

        public bool AvoidHighwaysToggled
        {
            get => _avoidHighways;
            set => _avoidHighways = value;
        }

        public bool AvoidFerriesToggled
        {
            get => _avoidFerries;
            set => _avoidFerries = value;
        }

        public int Consumption
        {
            get => _refConsumption;
            set => _refConsumption = value;
        }

        public int InitialSoc
        {
            get => _initialSoc;
            set
            {
                _initialSoc = value;

                if (value > 100)
                {
                    ((MasterDetailPage) Application.Current.MainPage).DisplayAlert("Invalid format", "Enter a value between 0 and 100", "Ok");
                    _initialSoc = 0;
                }
            }
        }

        public string Typecode
        {
            get => _typecode;
            set => _typecode = value;
        }

        public List<Vehicle> UserVehicles
        {
            get => _userVehicles;
            set => _userVehicles = value;
        }

        public List<string> UserVehiclesRegistrationNumbers
        {
            get => _userVehiclesRegistrationNumbers;
            set => _userVehiclesRegistrationNumbers = value;
        }

        public string SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                _selectedVehicle = value;
                if (value != null || !string.IsNullOrEmpty(_selectedVehicle))
                {
                    ObtainTypecodeByRegistrationNumber();
                }
            } 
        }

        public string TripName
        {
            get => _tripName;
            set => _tripName = value;
        }

        public List<string> UsedChargingStations
        {
            get => _usedChargingStations;
            set => _usedChargingStations = value;
        }

        public double Cost
        {
            get => _cost;
            set => _cost = value;
        }

        public long Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public List<IternioStep> TripPlan
        {
            get => _steps;
            set => _steps = value;
        }

        public string TotalTripDuration
        {
            get
            {
                TimeSpan ts = TimeSpan.FromSeconds(Duration);
                if(ts.Days == 0){
                    return string.Format("{0} h {1} min", ts.Hours, ts.Minutes);
                }
                else
                {
                    return string.Format("{0} days {1} h {2} min", ts.Days, ts.Hours, ts.Minutes);
                }
            }
        }

        private long _drivingDuration;
        public string TotalDrivingDuration
        {
            get
            {
                TimeSpan ts = TimeSpan.FromSeconds(_drivingDuration);
                if (ts.Days == 0)
                {
                    return string.Format("{0} h {1} min", ts.Hours, ts.Minutes);
                }
                else
                {
                    return string.Format("{0} days {1} h {2} min", ts.Days, ts.Hours, ts.Minutes);
                }
            }
        }

        private long _drivingDistance;
        public string TotalDrivingDistance
        {
            get
            {
                return _drivingDistance / 1000 + " km";
            }
        }

        private int _numberOfCharges;
        public string NumberOfCharges
        {
            get => _numberOfCharges.ToString() + " charges";
        }

        private long _chargingDuration;

        public string TotalChargingDuration
        {
            get
            {
                TimeSpan ts = TimeSpan.FromSeconds(_chargingDuration);
                if (ts.Days == 0)
                {
                    return string.Format("{0} h {1} min", ts.Hours, ts.Minutes);
                }
                else
                {
                    return string.Format("{0} days {1} h {2} min", ts.Days, ts.Hours, ts.Minutes);
                }
            }
        }

        public string TotalTripCost => Cost + " €";

        public List<TripDetailCell> TripDetails
        {
            get => _tripDetails;
            set => _tripDetails = value;
        }

        public string AuxOriginName
        {
            get => auxOriginName;
            set => auxOriginName = value;
        }

        public string AuxDestName
        {
            get => auxDestName;
            set => auxDestName = value;
        }


        public ICommand GetLocationNameCommand { get; set; }
        public bool IsRouteRunning => HasRouteRunning;
        
        public bool IsRouteNotRunning => !HasRouteRunning;

        public bool IsSaveAvailable => HasRouteRunning && !TripSaved;

        public bool HasFavorite => !Settings.Settings.FavRegistrationNumber.Equals("");
        public PlanTripViewModel()
        {
            LoadRouteCommand = new Command(async () => await LoadRoute());
            CancelTripCommand = new Command(CancelTrip);
            CleanFieldsCommand = new Command(ResetFields);
            VerifiyIfHasVehicles = new Command(UserHasVehicles);
            SaveTripCommand = new Command(SaveTrip);
            OpenTripOnMapCommand = new Command(OpenTripOnMap);
            FavoriteVehicleSelectedCommand = new Command(TypecodeFromFavorite);
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }
        

        public async Task LoadRoute()
        {
            if (string.IsNullOrEmpty(_originLatitude) || string.IsNullOrEmpty(_originLongitude) || string.IsNullOrEmpty(_destinationLatitude) || string.IsNullOrEmpty(_destinationLongitude))
            {
                await ((MasterDetailPage)App.Current.MainPage).DisplayAlert("Insuficient Parameters", "Please select an origin and destination!", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Typecode))
            {
                await ((MasterDetailPage)App.Current.MainPage).DisplayAlert("Vehicle not select", "Please select a vehicle for this trip!", "Ok");
                return;
            }
            
            var tripPlan = await iternioApi.GetTripPlan(_originLatitude, _originLongitude, _destinationLatitude,
                _destinationLongitude, _typecode, _initialSoc, _refConsumption, _avoidTolls, _avoidHighways, _avoidFerries);
            
            var steps = new List<IternioStep>();
            var stepsChargersIds = new List<string>();
            
            if (tripPlan != null && tripPlan.Result.Routes != null && tripPlan.Result.Routes.Length > 0)
            {
                TripPlan = tripPlan.Result.Routes[0].Steps.ToList();
                HasRouteRunning = true;
                

                // Iterate through all the charging stations and see the steps that are actual charging stations, so we can
                // display the map pins
                foreach (var step in tripPlan.Result.Routes[0].Steps)
                {
                    if (step.IsCharger)
                    {
                        steps.Add(step);
                        stepsChargersIds.Add(step.Charger.Name);
                        if (step.ChargeDuration != null && step.ChargeCost != null)
                            Cost += step.ChargeDuration.Value * step.ChargeCost.Value;
                        _numberOfCharges += 1;
                    }
                }

                UsedChargingStations = stepsChargersIds;
                Duration = tripPlan.Result.Routes[0].TotalChargeDuration + tripPlan.Result.Routes[0].TotalDriveDuration;
                _drivingDuration = tripPlan.Result.Routes[0].TotalDriveDuration;
                _drivingDistance = tripPlan.Result.Routes[0].TotalDist;
                _chargingDuration = tripPlan.Result.Routes[0].TotalChargeDuration;

                await ObtainGoogleMapsDirectionsRoute(steps);
            }
            else
            {
                await ((MasterDetailPage) App.Current.MainPage).Detail.DisplayAlert(":(", "No route found", "Ok");
            }
        }

        public async Task ObtainGoogleMapsDirectionsRoute(List<IternioStep> steps)
        {
            var googleDirection = await googleMapsApi.GetDirections(_originLatitude, _originLongitude, 
                _destinationLatitude, _destinationLongitude, steps);
            
            if(googleDirection.Routes!=null && googleDirection.Routes.Count>0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                
                _originLatitude =_originLatitude.Replace('.', ',');
                _originLongitude = _originLongitude.Replace('.', ',');
                _destinationLatitude = _destinationLatitude.Replace('.', ',');
                _destinationLongitude = _destinationLongitude.Replace('.', ',');

                CalculateRouteCommand.Execute(new TripPlanHelper(new Position(Convert.ToDouble(_originLatitude),
                    Convert.ToDouble(_originLongitude)), new Position(Convert.ToDouble(_destinationLatitude),
                    Convert.ToDouble(_destinationLongitude)), positions, steps));
                InitializeTripDetails();
            }
            else
            {
                await ((MasterDetailPage) App.Current.MainPage).Detail.DisplayAlert(":(", "No route found", "Ok");
            }
        }
        
        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult= places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count ==0);
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                if (_isPickupFocused)
                {
                    OriginText = place.Name;
                    _originLatitude = $"{place.Latitude}";
                    _originLongitude = $"{place.Longitude}";
                    _isPickupFocused = false;
                    FocusOriginCommand.Execute(null);
                    AuxOriginName = OriginText;
                }
                else
                {
                    _destinationLatitude = $"{place.Latitude}";
                    _destinationLongitude = $"{place.Longitude}";
                    AuxDestName = place.Name;

                    RecentPlaces.Add(placeA);

                    if (_originLatitude == _destinationLatitude && _originLongitude == _destinationLongitude)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Origin route should be different than destination route", "Ok");
                    }
                    else
                    {
                        _originLatitude =_originLatitude.Replace(',', '.');
                        _originLongitude = _originLongitude.Replace(',', '.');
                        _destinationLatitude = _destinationLatitude.Replace(',', '.');
                        _destinationLongitude = _destinationLongitude.Replace(',', '.');
                        await ((MasterDetailPage) Application.Current.MainPage).Detail.Navigation.PopAsync(false);

                        CleanSeachPlacePageData();
                    }
                }
            }
        }

        public void OpenTripOnMap()
        {
            var uriString = "https://www.google.pt/maps/dir/";
            foreach (var step in TripPlan)
            {
                var lat = step.Lat.ToString().Replace(',', '.');
                var lon = step.Lon.ToString().Replace(',', '.');
                uriString += lat + "," + lon + "/";
            }
            var uri = new Uri(uriString);
            Launcher.OpenAsync(uri);
        }

        public async void SaveTrip()
        {
            if (string.IsNullOrEmpty(TripName))
            {
                await ((MasterDetailPage) App.Current.MainPage).DisplayAlert("Invalid Name", "Please enter a valid trip name", "Ok");
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                
                PGB.Models.Position origin = new PGB.Models.Position(Convert.ToDouble(_originLatitude),
                    Convert.ToDouble(_originLongitude));
                PGB.Models.Position destination = new PGB.Models.Position(Convert.ToDouble(_destinationLatitude),
                    Convert.ToDouble(_destinationLongitude));

                TripSaved = true;
                Trip trip = new Trip(0, TripName, SelectedVehicle, origin, destination, TotalTripDuration, 
                    Cost, UsedChargingStations, dateTime);
                await pgbApiServices.AddTripAsync(Settings.Settings.Username, trip, Settings.Settings.AccessToken);
                
                await NavigationExtension.PopPopupAsync(null, true);
                await ((MasterDetailPage) App.Current.MainPage).DisplayAlert("Success", "Trip saved with success", "Ok");
            }
        }

        void CleanSeachPlacePageData()
        {
            OriginText = DestinationText = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
        }
        
        void CleanFields()
        {
            Typecode = SelectedVehicle = OriginText = DestinationText = _originLatitude = _originLongitude = 
                _destinationLatitude = _destinationLongitude = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
            AvoidTollsToggled = AvoidHighwaysToggled = AvoidFerriesToggled = false;
            InitialSoc = Consumption = 0;
            UserVehicles = null;
            UserVehiclesRegistrationNumbers = null;
            Duration = _drivingDuration = _drivingDistance = _numberOfCharges = 0;
            _chargingDuration = 0;
            TripPlan = null;
            Cost = 0.0;
            TripDetails = null;
        }

        void CancelTrip()
        {
            SelectedVehicle = TripName = Typecode = OriginText = DestinationText = _originLatitude = 
                _originLongitude = _destinationLatitude = _destinationLongitude = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
            AvoidTollsToggled = AvoidHighwaysToggled = AvoidFerriesToggled = false;
            HasRouteRunning = TripSaved = false;
            InitialSoc = Consumption = 0;
            UserVehicles = null;
            UserVehiclesRegistrationNumbers = null;
            Duration = 0;
            Cost = 0;
            UsedChargingStations = null;
            Duration = _drivingDuration = _drivingDistance = _numberOfCharges = 0;
            _chargingDuration = 0;
            TripPlan = null;
            Cost = 0.0;
            TripDetails = null;
            AuxDestName = AuxOriginName = string.Empty;
        }

        async void ResetFields()
        {
            await Application.Current.MainPage.DisplayAlert("Fields cleared", "All cleared with success", "Ok");
        }

        void TypecodeFromFavorite()
        {
            Typecode = Settings.Settings.TypeCode;
        }

        public void InitializeTripDetails()
        {
            _tripDetails = new List<TripDetailCell>();

            foreach (var step in TripPlan)
            {

                TripDetailCell tripDetailCell = new TripDetailCell();

                if (step.ArrivalPerc == step.DeparturePerc)
                {
                    if (step.DepartureDist > 0)
                    {
                        tripDetailCell.Name = AuxOriginName;
                        tripDetailCell.ImageSource = "origin_pin.png";
                        tripDetailCell.HasDrivingNext = true;
                    }
                    else
                    {
                        tripDetailCell.Name = auxDestName;
                        tripDetailCell.ImageSource = "destination_pin.png";
                        tripDetailCell.HasDrivingNext = false;
                    }
                    tripDetailCell.Percentage = step.ArrivalPerc + "%";
                    tripDetailCell.HasClock = false;
                }
                if (step.IsCharger)
                {
                    tripDetailCell.Name = step.Charger.Name;
                    tripDetailCell.HasClock = true;
                    tripDetailCell.ImageSource = "charging_station_pin.png";
                    if (step.ChargeDuration != null)
                    {
                        TimeSpan ts1 = TimeSpan.FromSeconds(step.ChargeDuration.Value);
                        if (ts1.Days == 0)
                        {
                            tripDetailCell.Duration = string.Format("{0} h {1} min", ts1.Hours, ts1.Minutes);
                        }
                        else
                        {
                            tripDetailCell.Duration = string.Format("{0} days {1} h {2} min", ts1.Days, ts1.Hours, ts1.Minutes);
                        }

                    }

                    tripDetailCell.Percentage = step.ArrivalPerc + "% -> " + step.DeparturePerc + "%";
                    tripDetailCell.HasDrivingNext = true;
                }
                
                if (step.DriveDuration != null && step.DriveDist != null && tripDetailCell.HasDrivingNext)
                {
                    TimeSpan ts2 = TimeSpan.FromSeconds(step.DriveDuration.Value);
                    if (ts2.Days == 0)
                    {
                        tripDetailCell.DrivingNext = string.Format("{0} h {1} min", ts2.Hours, ts2.Minutes) + $" ({step.DriveDist.Value / 1000} km)";
                    }
                    else
                    {
                        tripDetailCell.DrivingNext =
                            string.Format("{0} days {1} h {2} min", ts2.Days, ts2.Hours, ts2.Minutes) +
                            $" ({step.DriveDist.Value / 1000} km)";
                    }
                }
                
                _tripDetails.Add(tripDetailCell);
            }
        }

        //Get place 
        public async Task GetLocationName(Position position)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    OriginText = placemark.FeatureName;
                }
                else
                {
                    OriginText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }


        public async void UserHasVehicles()
        {
            UserVehicles = await pgbApiServices.GetVehiclesAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            if (UserVehicles == null || UserVehicles.Count == 0)
            {
                await ((MasterDetailPage) Application.Current.MainPage).DisplayAlert("No Vehicles", "You don't have any registered vehicles, please register one", "Ok");
                await ((MasterDetailPage) Application.Current.MainPage).Detail.Navigation.PopAsync(false);
            }
            else
            {
                List<string> registrationNumbers = new List<string>();
                foreach (var vehicle in UserVehicles)
                {
                    registrationNumbers.Add(vehicle.RegistrationNumber);
                }

                UserVehiclesRegistrationNumbers = registrationNumbers;
            }
        }

        public void ObtainTypecodeByRegistrationNumber()
        {
            foreach (var vehicle in UserVehicles)
            {
                if (vehicle.RegistrationNumber.Equals(SelectedVehicle))
                {
                    Typecode = vehicle.TypeCode;
                    return;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
