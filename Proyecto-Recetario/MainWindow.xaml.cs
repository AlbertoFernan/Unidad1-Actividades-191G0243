using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Recetario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Filtro.Text == "")
            {
                ListaRecetas.FiltrarCommand.Execute(null);
            }
            else
            {
                ListaRecetas.FiltrarCommand.Execute(Filtro.Text);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            ListaRecetas.rece = Filtro.Text;
            if (Filtro.Text=="")
            {
                ListaRecetas.FiltrarCommand.Execute(null);
            }
            else
            {
                ListaRecetas.FiltrarCommand.Execute(Filtro.Text);
            }
        }
    }
}

