using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PGB.Models;
using PGB.Pages;
using PGB.Services;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class FavoriteStationsViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        private ObservableCollection<ExternalChargingStationOCM> _favorites;
        public ObservableCollection<ExternalChargingStationOCM> Favorites
        {
            get { return _favorites; }
            set
            {
                if (_favorites != value)
                    _favorites = value;
                OnPropertyChanged("Favorites");
            }
        }

        public ICommand Delete { protected set; get; }

        public ICommand Detailed { protected set; get; }

        public async void GetFavorites()
        {
            List<string> favs = await ApiServices.GetFavoritesAsync(Settings.Settings.Username, Settings.Settings.AccessToken);
            IList<long> favsLong = new List<long>();

            foreach (string s in favs)
            {
                favsLong.Add(Convert.ToInt64(s));
            }
            Root root = (Root)Application.Current.MainPage;

            // Pegar nas charging stations para dar match, neste caso ir buscar as charging stations no sql com os longs em favsLong
            List<ExternalChargingStationOCM> cs = (List<ExternalChargingStationOCM>) root.GetChargingStations();
            if (cs.Count > 0)
            {
                foreach (ExternalChargingStationOCM c in cs)
                {
                    if (favsLong.Contains(c.Id) && !Favorites.Contains(c))
                    {
                        Favorites.Add(c);
                        favsLong.Remove(c.Id);
                    }
                }
            }
        }

        public FavoriteStationsViewModel()
        {
           
            Favorites = new ObservableCollection<ExternalChargingStationOCM>();

            GetFavorites();

         
            Delete = new Command<ExternalChargingStationOCM>(async (chargingStation) =>
            {
                //opreate the remove item

                bool answer = await Application.Current.MainPage.DisplayAlert("Confirmation", "Do you pretend to delete charging stattion " + chargingStation.AddressInfo.Title + "?", "Yes", "No");
                if (answer)
                {
                    //opreate the remove item
                    Favorites.Remove(chargingStation);
                    await ApiServices.DeleteFavoriteAsync(Settings.Settings.Username, chargingStation.Id.ToString(), Settings.Settings.AccessToken);
                }
            });

            Detailed = new Command<ExternalChargingStationOCM>(async (chargingStation) =>
            {   
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("ChargingStation", chargingStation);
                root.Hide();
            });
        }
    }
}
