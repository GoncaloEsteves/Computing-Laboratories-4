﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGB.Pages{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        public LogIn(){

            InitializeComponent();
        }
        void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Application.Current.MainPage = new Register();
        }
    }
}