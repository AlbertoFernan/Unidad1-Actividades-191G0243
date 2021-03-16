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

namespace Unidad_2_actividad_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el capitulo?", "Confirme", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListaEpisodios.EliminarCommand.Execute(null); //Ejecutar el command
                //listadoAlumnos.Eliminar();
            }
        }
    }
}
