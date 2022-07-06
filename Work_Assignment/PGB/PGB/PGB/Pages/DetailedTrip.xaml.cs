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
    public partial class DetailedTrip : ContentPage
    {
        public DetailedTrip(Trip trip)
        {
            InitializeComponent();

            var detailedTripViewModel = new DetailedTripViewModel();

            detailedTripViewModel.Trip = trip;

            BindingContext = detailedTripViewModel;
        }

        void GoBack(object sender, EventArgs args)
        {
            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("MyTrips");
            root.Hide();
        }
    }
}