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
    public partial class RegisterVehicle : ContentPage
    {
        public RegisterVehicle()
        {
            InitializeComponent();
        }

        void Cancel(object sender, EventArgs args)
        {

            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("MyVehicles");
            root.Hide();
        }
    }
}