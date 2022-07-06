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
    public partial class MyVehicles : ContentPage
    {
        public MyVehicles()
        {
            InitializeComponent();
        }

        void AddVehicle(object sender, EventArgs args)
        {

            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("RegisterVehicle");
            root.Hide();
        }
    }
}