using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto1Arterial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaRegistrosView());
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
