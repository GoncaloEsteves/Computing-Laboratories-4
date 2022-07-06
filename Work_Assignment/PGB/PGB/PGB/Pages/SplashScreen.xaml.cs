using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Start : ContentPage
    {
        public Start()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            ImageButton button = (ImageButton)sender;
            Application.Current.MainPage = new LogIn();
        }
    }
}