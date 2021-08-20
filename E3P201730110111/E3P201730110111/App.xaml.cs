using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace E3P201730110111
{
    public partial class App : Application
    {
        
        public static string UbicacionDB = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        

        public App(string DBlocal)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            UbicacionDB = DBlocal;
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
    }
}
