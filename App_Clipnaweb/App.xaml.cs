using App_Clipnaweb.Views.pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Clipnaweb
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashPage());
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
