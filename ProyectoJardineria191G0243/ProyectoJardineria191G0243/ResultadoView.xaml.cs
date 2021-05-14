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
    public partial class ResultadoView : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        public ResultadoView()
        {
            InitializeComponent();
        }
    }
}