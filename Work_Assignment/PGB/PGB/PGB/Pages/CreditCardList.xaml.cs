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
    public partial class CreditCardList : ContentPage
    {
        public CreditCardList()
            {
                InitializeComponent();
            }

            void AddCreditCard(object sender, EventArgs args)
            {

                ImageButton button = (ImageButton)sender;
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("AddCreditCard");
                root.Hide();
            }

        void GoProfile(object sender, EventArgs args)
        {

            ImageButton button = (ImageButton)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("Profile");
            root.Hide();
        }
    }
}