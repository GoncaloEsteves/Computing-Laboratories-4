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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        void GoToEditProfile(object sender, EventArgs args)
        {
            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("EditProfile");
            root.Hide();
        }
        void GoToCreditCards(object sender, EventArgs args)
        {
            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("CreditCards");
            root.Hide();
        }
    }
}