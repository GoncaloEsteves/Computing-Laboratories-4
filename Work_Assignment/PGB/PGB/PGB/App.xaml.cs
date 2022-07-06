using PGB.Data;
using PGB.Pages;
using PGB.Services;
using System;
using Xamarin.Forms;

namespace PGB
{
    public partial class App : Application
    {
        static PGBDatabase database;
        
        public App()
        {
            InitializeComponent();
            IternioApiService.Initialize(Constants.IternioPlanningApiKey);
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new Start();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
        public static PGBDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PGBDatabase();
                }
                return database;
            }
        }
    }
}
