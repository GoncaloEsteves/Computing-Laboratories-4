using PGB.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class AddCreditCard : ContentPage
    {
        public AddCreditCard()
        {
            InitializeComponent();
            this.BindingContext = new AddCreditCardViewModel();
        }

        void Cancel(object sender, EventArgs args)
        {

            Button button = (Button)sender;
            Root root = (Root)Application.Current.MainPage;
            root.SetDetail("CreditCards");
            root.Hide();
        }
    }
}