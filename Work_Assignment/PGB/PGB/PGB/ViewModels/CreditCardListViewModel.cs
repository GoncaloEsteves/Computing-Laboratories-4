using PGB.Models;
using PGB.Pages;
using PGB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class CreditCardListViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private ObservableCollection<CreditCard> creditCards;
        public ObservableCollection<CreditCard> CreditCards
        {
            get { return creditCards; }
            set
            {
                if (creditCards != value)
                    creditCards = value;
                OnPropertyChanged("CreditCards");
            }
        }

        public ICommand Delete { protected set; get; }

        private async void BuildList()
        {
            List<CreditCard> aux = await ApiServices.GetCreditCardsAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
 
            foreach (CreditCard creditcard in aux)
            {
                CreditCards.Add(creditcard);
            }
        }

        public CreditCardListViewModel()
        {

            CreditCards = new ObservableCollection<CreditCard>();

            BuildList();
            
            Delete = new Command<CreditCard>(async (creditcard) =>
            {
                //await Application.Current.MainPage.DisplayAlert("Working", "To do", "OK");
                
                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete credit card with the number " + creditcard.CardNumber + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    CreditCards.Remove(creditcard);
                    await _apiServices.DeleteCreditCardAsync(Settings.Settings.Username, creditcard.CardNumber, Settings.Settings.AccessToken);
                }
            });
        }
    }
}
