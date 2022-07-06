using PGB.Models;
using PGB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedVehicle : ContentPage
    {
        public DetailedVehicle(Vehicle vehicle)
        {
            InitializeComponent();

            var detailedVehicleViewModel = new DetailedVehicleViewModel();

            detailedVehicleViewModel.Vehicle = vehicle;

            detailedVehicleViewModel.Fav = vehicle.RegistrationNumber.Equals(Settings.Settings.FavRegistrationNumber);

            BindingContext = detailedVehicleViewModel;
        }

        void GoBack(object sender, EventArgs args)
        {
            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("MyVehicles");
            root.Hide();
        }
    }
}