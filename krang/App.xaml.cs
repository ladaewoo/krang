using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace krang
{
    public partial class App : Application
    {
        public App()
        {
           
            Device.InvokeOnMainThreadAsync(() =>
            {
                InitializeComponent();
                MainPage = new MainPage();
            });
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
