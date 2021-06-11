
using Proyecto1Presion.Models;
using Proyecto1Presion.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto1Presion.ViewModels
{
    public class PresionViewModel : INotifyPropertyChanged
    {

        //Commands
        public ICommand NuevoReCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand { get; set; }

        public ICommand GuardarCommand { get; set; }

        public ICommand CancelarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }

        //Enlaces a objetos

        public ObservableCollection<PresionArterial> Presiones { get; set; } = new ObservableCollection<PresionArterial>();

        private PresionArterial presion;

        public PresionArterial Presion
        {
            get { return presion; }
            set { presion = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MensajeError))); }
        }

        private string mensajeerror;

        public string MensajeError
        {
            get { return mensajeerror; }
            set
            {
                mensajeerror = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MensajeError)));

            }
        }


        //Vistas

        
        AgregarRegistroView AgregarView;
        EditarRegistroView EditarView;


        //Clases DAL
        RegistroPresion Registro = new RegistroPresion();

        public event PropertyChangedEventHandler PropertyChanged;

        public PresionViewModel()
        { 
          
            EditarView = new EditarRegistroView() { BindingContext = this };
            

           
            NuevoReCommand = new Command(NuevoReg);
            EditarCommand = new Command(Editar);
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Regresar);

            AgregarCommand = new Command(Agregar);
            Cargar();
        }



        private void Cargar()
        {
            Presiones.Clear();
            foreach (var cuentos in Registro.GetAll())
            {
                Presiones.Add(cuentos);
            }
        }


        private void NuevoReg()
        {
            Presion = new PresionArterial();
            AgregarView = new AgregarRegistroView() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(AgregarView);
        }



        private void Agregar()

        {
            MensajeError = "";
            if (Registro.Insert(Presion))
            {
                // FiltroFecha();

                App.Current.MainPage.Navigation.PopAsync();

                Cargar();


            }
            else
            {
                MensajeError = string.Join("\n", Registro.ListaErrores);
            }
        }

        private void Editar()
        {
            PresionArterial clon = new PresionArterial { Id = Presion.Id, Fecha = Presion.Fecha, Nombre = Presion.Nombre, Hora = Presion.Hora, PresionDiastolica = Presion.PresionDiastolica, PresionSistolica = Presion.PresionSistolica, Pulso = Presion.Pulso };
            Presion = clon;

           App.Current.MainPage.Navigation.PushAsync(EditarView, false);

        }

        private void Regresar()
        {
            App.Current.MainPage.Navigation.PopAsync();
            Presion = null;
        }




        private void Guardar()
        {
            MensajeError = "";

            if (Registro.Update(Presion))
            {

                App.Current.MainPage.Navigation.PopAsync();
                Cargar();
            }
            else
            {
                MensajeError = string.Join("\n", Registro.ListaErrores);
            }


        }

    }
}
