using System;

using Xamarin.Forms;

namespace PGB.Pages
{
    public partial class PlanTripSettings : ContentPage
    {
        public PlanTripSettings()
        {
            InitializeComponent();
        }

        private void PickerLabel_OnTapped(object sender, EventArgs e)
        {
            VehiclesPicker.Focus();
        }

        private void VehicleList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (VehiclesPicker.SelectedIndex != -1)
            {
                PickerLabel.Text = VehiclesPicker.Items[VehiclesPicker.SelectedIndex];
            }
        }
    }
}
