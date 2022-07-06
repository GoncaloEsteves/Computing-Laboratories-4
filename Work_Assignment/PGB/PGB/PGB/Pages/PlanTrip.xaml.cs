using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PGB.Pages
{
    public partial class PlanTrip : ContentPage
    {
        public PlanTrip()
        {
            InitializeComponent();
        }
        
        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            await ((MasterDetailPage) Application.Current.MainPage).Detail.Navigation.PushAsync(
                new SearchPlacePage() {BindingContext = this.BindingContext}, false);
        }

        public async void OnSettingsClicked(object sender, EventArgs e)
        {
            await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(
                new PlanTripSettings() { BindingContext = this.BindingContext }, false);
        }
        
        private async void OnTripDetailsClicked(object sender, EventArgs e)
        {
            await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(
                new TripPlanDetails() { BindingContext = this.BindingContext }, false);
        }
        
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SaveTripPopupPage() {BindingContext = this.BindingContext});
        }

        public void Handle_Cancel(object sender, EventArgs e)
        {
            searchLayout.IsVisible = true;
            Map.Polylines.Clear();
            Map.Pins.Clear();
        }

        //Center map in actual location
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Map.GetActualLocationCommand.Execute(null);
        }

        void OnCalculate(Object sender, EventArgs e)
        {
            searchLayout.IsVisible = false;
            
        }

        async void OnLocationError(Object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Unable to get actual location", "Ok");
        }
    }
}