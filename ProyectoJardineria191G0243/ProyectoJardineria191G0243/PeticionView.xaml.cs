using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJardineria191G0243
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeticionView : ContentPage
    {

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        public PeticionView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}