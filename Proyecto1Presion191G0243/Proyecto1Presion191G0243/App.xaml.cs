using System;
using Xamarin.Forms;
using Proyecto1Presion191G0243.Models;
using Xamarin.Forms.Xaml;

namespace Proyecto1Presion191G0243
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        
            MainPage = new NavigationPage(new Views.AgregarRegistroView());
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
