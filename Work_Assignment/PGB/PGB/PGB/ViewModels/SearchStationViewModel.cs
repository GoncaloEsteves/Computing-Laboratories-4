using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using PGB.Models;
using PGB.Pages;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class SearchStationViewModel : BaseViewModel
    {

        public IList<ExternalChargingStationOCM> ChargingStations;

        private ObservableCollection<ExternalChargingStationOCM> result;
        public ObservableCollection<ExternalChargingStationOCM> Result
        {
            get { return result; }
            set
            {
                if (result != value)
                    result = value;
                OnPropertyChanged("Result");
            }
        }

        public ICommand Detailed { protected set; get; }

        private bool NotInList(ExternalChargingStationOCM c)
        {
            foreach (ExternalChargingStationOCM stations in Result)
            {
                if (stations.Id == c.Id )
                {
                    return false;
                }
            }
            return true;
        }

        public ICommand PerformSearch => new Command<string>(async(string query) =>
        {
            Result.Clear();
            if (ChargingStations.Count > 0)
            {
                if (query.Length >= 1)
                {
                    foreach (ExternalChargingStationOCM c in ChargingStations)
                    {
                        if (c.AddressInfo.Title.ToLower().Contains(query.ToLower()) && c.AddressInfo != null && c.AddressInfo.Title != null && NotInList(c))
                        {
                            Result.Add(c);
                        }
                    }
                }
            }
        });

        public SearchStationViewModel(){

            Result = new ObservableCollection<ExternalChargingStationOCM>();

            Detailed = new Command<ExternalChargingStationOCM>(async (selected) =>
            {
                Root root = (Root)Application.Current.MainPage;
                root.SetDetail("ChargingStation", selected);
                root.Hide();
            });
        }
    }
}
