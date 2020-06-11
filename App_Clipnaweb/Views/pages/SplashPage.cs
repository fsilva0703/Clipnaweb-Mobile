using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App_Clipnaweb.Views.pages
{
    public class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logovc.png",
                WidthRequest = 100,
                HeightRequest = 100
            };

            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.White;
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1.2, 2000);
            await splashImage.ScaleTo(0.13, 1000, Easing.Linear);
            await splashImage.ScaleTo(1.5, 500, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new Login()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
        }
    }
}
