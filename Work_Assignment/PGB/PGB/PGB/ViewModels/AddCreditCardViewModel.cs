using PGB.Converters;
using PGB.Models;
using PGB.Pages;
using PGB.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class AddCreditCardViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private string cardNumber;
        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                if (cardNumber != value)
                    cardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }
        private string cardCvv;
        public string CardCvv
        {
            get { return cardCvv; }
            set
            {
                if (cardCvv != value)
                    cardCvv = value;
                OnPropertyChanged("CardCvv");
                var len = CardCvv.Length;

                StringBuilder sb = new StringBuilder();
                for (uint i = 0; i < len; i++)
                    sb.Append("*");

                CardCvvPoints = sb.ToString();
            }
        }

        private string cardCvvPoints;
        public string CardCvvPoints
        {
            get { return cardCvvPoints; }
            set
            {
                if (cardCvvPoints != value)
                    cardCvvPoints = value;
                OnPropertyChanged("CardCvvPoints");
            }
        }

        private string cardExpirationDate;
        public string CardExpirationDate
        {
            get { return cardExpirationDate; }
            set
            {
                if (cardExpirationDate != value)
                    cardExpirationDate = value;
                OnPropertyChanged("CardExpirationDate");
            }
        }

        public ICommand Add
        {
            get
            {
                return new Command(async () =>
                {
                    bool invalid = false;
                    if (CardExpirationDate == null || !CardExpirationDate.Contains("/"))
                    {
                        invalid = true;
                    } 
                    if (CardCvv == null || CardNumber == null || CardExpirationDate == null || invalid)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Insert all credentials", "OK");
                    } else
                    {
                        if(CardNumber.Length != 19 || CardCvv.Length < 3)
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Card Number must have 16 digits and cvv must have 3 digits", "OK");
                        } else
                        {
                            //call register in api and handle errors 
                            string[] words;
                            int year;
                            int month;
                            words = CardExpirationDate.Split('/');
                            year = Convert.ToInt32("20" + words[1]);
                            month = Convert.ToInt32(words[0]);

                            if (month > 12 || year < 2020)
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", "Insert expiration date", "OK");
                            }
                            else
                            {
                                CreditCard card = new CreditCard(NumberToType(CardNumber), CardNumber, StringToDateTime(CardExpirationDate), Convert.ToInt32(CardCvv));
                                var isAdded = await ApiServices.AddCreditCardAsync(Settings.Settings.Username, card, Settings.Settings.AccessToken);
                                if (isAdded)
                                {
                                    await Application.Current.MainPage.DisplayAlert("Success", "Credit card added with success", "OK");
                                    Root root = (Root)Application.Current.MainPage;
                                    root.SetDetail("CreditCards");
                                    root.Hide();
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert("Alert", "Credit Card already exists.", "OK");
                                }
                            }
                        }
                    }
                });
            }
        }

        private string NumberToType(string numberCard)
        {
            if (numberCard == null) return "NotRecognized";

            var number = numberCard.ToString();
            var numberNormalized = number.Replace("-", string.Empty);

            if (BaseCardValidator.visaRegex.IsMatch(numberNormalized)) return "Visa";

            if (BaseCardValidator.amexRegex.IsMatch(numberNormalized)) return "Amex";

            if (BaseCardValidator.masterRegex.IsMatch(numberNormalized)) return "MasterCard";

            if (BaseCardValidator.dinnersRegex.IsMatch(numberNormalized)) return "Dinners";

            if (BaseCardValidator.discoverRegex.IsMatch(numberNormalized)) return "Discover";

            if (BaseCardValidator.jcbRegex.IsMatch(numberNormalized)) return "JCB";

            return "NotRecognized";
        }

        private int NumberToInt(string numberCard)
        {
            string[] words = numberCard.Split('-');
            StringBuilder sb = new StringBuilder(); ;
            foreach (string s in words){
                sb.Append(s);
            }
            return Convert.ToInt32(sb.ToString());

        }

        private DateTime StringToDateTime(string date)
        {

            string[] words = date.Split('/');
            string year = "0";
            string month = "0";

            if(words[1] != null)
            {
                year = "20" + words[1];
            }

            if (words[0] != null)
            {
                month = words[0];
            }

            var result = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1, 0, 0, 0);
            //await Application.Current.MainPage.DisplayAlert("Success", result.ToString(), "OK");
            return result ;
        }
    }
}
