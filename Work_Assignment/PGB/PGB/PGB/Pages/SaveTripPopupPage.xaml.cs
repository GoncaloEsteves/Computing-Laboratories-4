using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

namespace PGB.Pages
{
    public partial class SaveTripPopupPage :  PopupPage
    {
        public SaveTripPopupPage()
        {
            InitializeComponent();
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}