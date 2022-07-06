using PGB.Models;
using PGB.ViewModels;
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
    public partial class SearchStation : ContentPage
    {
        public SearchStation(IList<ExternalChargingStationOCM> _chargingStations)
        {
            InitializeComponent();

            var searchstationViewModel = new SearchStationViewModel();

            searchstationViewModel.ChargingStations = _chargingStations;

            BindingContext = searchstationViewModel;
        }
    }
}