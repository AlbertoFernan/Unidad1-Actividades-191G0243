using Proyecto1Arterial.Models;
using Proyecto1Arterial.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto1Arterial.ViewModels
{
    public class PresionViewModel : INotifyPropertyChanged
    {

        
        public ICommand NuevoReCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand { get; set; }

        public ICommand GuardarCommand { get; set; }

        public ICommand CancelarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }

       

        public ObservableCollection<PresionArterial> Presiones { get; set; } = new ObservableCollection<PresionArterial>();

        private PresionArterial presion;

        public PresionArterial Presion
        {
            get { return presion; }
            set { presion = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Presion))); }
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

       

        public PresionViewModel()
        {
            NuevoReCommand = new Command(NuevoReg);
            EditarCommand = new Command<PresionArterial>(Editar);
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Regresar);
            AgregarCommand = new Command(Agregar);
            EliminarCommand = new Command<PresionArterial>(NavegarEliminar);

            AgregarView = new AgregarRegistroView() { BindingContext = this };

            EditarView = new EditarRegistroView() { BindingContext = this };




            Cargar();
        }


        private async void NavegarEliminar(PresionArterial obj)
        {
            var resultado = await Application.Current.MainPage
                .DisplayActionSheet("Atención ", "No", "Si", "¿Esta seguro de que quiere eliminar el registro?");

            if (resultado == "Si")
            {

                Presion = obj;
                Eliminar();
            }
        }

        private void Eliminar()
        {
            Registro.Delete(Presion);
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
            App.Current.MainPage.Navigation.PushAsync(AgregarView);
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

        private void Editar(PresionArterial P)
        {
            MensajeError = "";
            PresionArterial clon = new PresionArterial { Id = P.Id, Fecha = P.Fecha, Nombre = P.Nombre, Hora = P.Hora, PresionDiastolica = P.PresionDiastolica, PresionSistolica = P.PresionSistolica, Pulso = P.Pulso };
            Presion = clon;

            App.Current.MainPage.Navigation.PushAsync(EditarView);

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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
