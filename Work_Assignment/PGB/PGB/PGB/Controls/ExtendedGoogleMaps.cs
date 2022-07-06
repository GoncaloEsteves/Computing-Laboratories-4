using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using PGB.Helpers;
using PGB.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Polyline = Xamarin.Forms.GoogleMaps.Polyline;
using Position = Xamarin.Forms.GoogleMaps.Position;


namespace PGB.Controls
{
public class ExtendedMap : Xamarin.Forms.GoogleMaps.Map
    {
        public static readonly BindableProperty CalculateCommandProperty =
            BindableProperty.Create(nameof(CalculateCommand), typeof(ICommand), typeof(PlanTrip), null, BindingMode.TwoWay);

        public ICommand CalculateCommand
        {
            get { return (ICommand)GetValue(CalculateCommandProperty); }
            set { SetValue(CalculateCommandProperty, value); }
        }

        public static readonly BindableProperty UpdateCommandProperty =
          BindableProperty.Create(nameof(UpdateCommand), typeof(ICommand), typeof(PlanTrip), null, BindingMode.TwoWay);

        public ICommand UpdateCommand
        {
            get { return (ICommand)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }

        public static readonly BindableProperty GetActualLocationCommandProperty =
          BindableProperty.Create(nameof(GetActualLocationCommand), typeof(ICommand), typeof(PlanTrip), null, BindingMode.TwoWay);

        public ICommand GetActualLocationCommand
        {
            get { return (ICommand)GetValue(GetActualLocationCommandProperty); }
            set { SetValue(GetActualLocationCommandProperty, value); }
        }

        public event EventHandler OnCalculate = delegate {};
        public event EventHandler OnLocationError = delegate {};

        public ExtendedMap()
        {
            AddMapStyle();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext != null)
            {
                CalculateCommand = new Command<TripPlanHelper>(Calculate);
                UpdateCommand = new Command<Position>(Update);
                GetActualLocationCommand = new Command(async () => await GetActualLocation());
            }
        }

        void AddMapStyle()
        {
            var assembly = typeof(ExtendedMap).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"PGB.Styles.MapStyle.json");
            string styleFile;
            using (var reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            MapStyle = MapStyle.FromJson(styleFile);
        }


        async void Update(Position position)
        {
            if (Pins.Count == 1 && Polylines != null && Polylines?.Count > 1)
                return;

            var cPin = Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Position(position.Latitude, position.Longitude);
                cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_taxi.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 });

                await MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                var previousPosition = Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                //END TRIP
                Polylines?.FirstOrDefault()?.Positions?.Clear();
            }


        }

        async void Calculate(TripPlanHelper tripPlanHelper)
        {
            OnCalculate?.Invoke(this, default(EventArgs));
            Polylines.Clear();

            foreach (var step in tripPlanHelper.Steps)
            {
                var chargingStationPin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(step.Lat, step.Lon),
                    Label = step.Charger.Name,
                    Address = step.Charger.Address
                };

                var fast = false;
                // Determine the charging type, if its fast or normal
                foreach (var outlet in step.Charger.Outlets)
                {
                    if (outlet.Power > 22)
                    {
                        fast = true;
                        break;
                    }
                }

                if (fast)
                {
                    chargingStationPin.Icon = BitmapDescriptorFactory.FromBundle("fast_charger_3.png");
                }
                else
                {
                    chargingStationPin.Icon = BitmapDescriptorFactory.FromBundle("normal_charger_3.png");
                }

                Pins.Add(chargingStationPin);
            }
            
            var polyline = new Polyline();
            foreach (var p in tripPlanHelper.Polylines)
            {
                polyline.Positions.Add(p);
                
            }

            polyline.StrokeColor = Color.LightSeaGreen;
            polyline.StrokeWidth = 5f;
            Polylines.Add(polyline);
            MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.50f)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Origin",
                Address = "Origin",
                Tag = string.Empty,
                //Icon = BitmapDescriptorFactory.FromBundle("origin_pin_32.png")
            };
            Pins.Add(pin);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
                Label = "Destination",
                Address = "Destination",
                Tag = string.Empty,
                //Icon = BitmapDescriptorFactory.FromBundle("destination_pin_32.png")
            };
            Pins.Add(pin1);
            
            await MoveCamera(CameraUpdateFactory.NewPositionZoom(new Position(tripPlanHelper.Origin.Latitude, tripPlanHelper.Origin.Longitude), -5.0));
        }

        async Task GetActualLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(location.Latitude, location.Longitude), Distance.FromKilometers(200)));

                }
            }
            catch (Exception ex)
            {
                OnLocationError?.Invoke(this, default(EventArgs));
            }
        }
    }
}