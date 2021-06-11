using GalaSoft.MvvmLight.Command;
using Proyecto2Agenda.Models;
using Proyecto2Agenda.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proyecto2Agenda.ViewModels
{
    public class AgendaViewModel : INotifyPropertyChanged
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


        //Enlaces objestos
        public ObservableCollection<Cita> Citas { get; set; } = new ObservableCollection<Cita>();

      

        public ObservableCollection<Cita> CitasFil { get; set;  } = new ObservableCollection<Cita>();

        private Cita cita;

        public Cita Cita
        {
            get { return cita; }
            set { cita = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cita))); }

        }

        private Control controlvista;

        public Control Controlvista
        {
            get { return controlvista; }
            set
            {
                controlvista = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Controlvista)));

            }
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
        ListaCitasView Listaview;
        EditarView Editarview;

        AgregarCitaView Agregarview;

        //Dal
        Agenda Registro = new Agenda();

        public event PropertyChangedEventHandler PropertyChanged;

        public AgendaViewModel()
        {
            Listaview = new ListaCitasView() { DataContext = this };
            Agregarview = new AgregarCitaView() { DataContext = this };
            Editarview = new EditarView() { DataContext = this };
            Controlvista = Listaview;
            Fechfil = null;
            EditarCommand = new RelayCommand(Editar);
            GuardarCommand = new RelayCommand(Guardar);
            CancelarCommand = new RelayCommand(Regresar);
            NuevoReCommand = new RelayCommand(Nuevo);
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(NavegarEliminar);

            ResetCommand = new RelayCommand(Reset);

            FiltrarCommand = new RelayCommand(FiltroFecha);
            CargarDatos();
            FiltroFecha();

        }
        private void NavegarEliminar()
        {

            if (MessageBox.Show("¿Borrar registro?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
              
                Eliminar();
               
            }
           
        
        }

        private void Eliminar()
        {
            Registro.Delete(Cita);
            CargarDatos();
            FiltroFecha();
        }
        private void Nuevo()
        {
            Cita = new Cita();
            Controlvista = Agregarview;

        }
        private void Reset()
        {
            Fechfil = null;
            FiltroFecha();
        }

        private DateTime? fechfill;

        public DateTime? Fechfil
        {
            get { return fechfill; }
            set { fechfill = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fechfil))); }
        }

        private void FiltroFecha()
        {
            CitasFil.Clear();
            var CitFil = Citas.Where(x => Fechfil == null || x.Fecha == Fechfil).OrderBy(x => x.Fecha);
            foreach (var Cita in CitFil)
            {
                CitasFil.Add(Cita);
            }
        }

        private void Agregar()

        {
           
            MensajeError = "";
            if (Registro.Insert(Cita))
            {
                CargarDatos();
                FiltroFecha();
                Controlvista = Listaview;
                



            }
            else
            {
                MensajeError = string.Join("\n", Registro.ListaErrores);
            }
        }

        private void Editar()
        {
            MensajeError = "";
            Cita clon = new Cita { Id = Cita.Id, Fecha = Cita.Fecha, Nombre = Cita.Nombre, Negocio = Cita.Negocio };
            Cita = clon;

            Controlvista = Editarview;

        }

        private void Regresar()
        {
            MensajeError = "";
            Controlvista = Listaview;
            Cita = null;
        }


        private void CargarDatos()
        {
            
            Citas.Clear();
            foreach (var citas in Registro.GetAll())
            {
                Citas.Add(citas);
            }
        }
        private void Guardar()
        {
            MensajeError = "";

            if (Registro.Update(Cita))
            {
                CargarDatos();
                FiltroFecha();
                Controlvista = Listaview;
                
            }
            else
            {
                MensajeError = string.Join("\n", Registro.ListaErrores);
            }


        }

    }
}
