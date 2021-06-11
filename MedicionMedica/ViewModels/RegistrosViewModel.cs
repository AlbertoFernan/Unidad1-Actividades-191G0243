using MedicionMedica.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicionMedica.ViewModels
{
    public class RegistrosViewModel
    {
        //Comandos

        public ICommand NuevoReCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand{ get; set; }

        public ICommand CancelarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand VerCommand { get; set; }


        //Enlaces objestos
        public ObservableCollection<Presion_arterial> Presiones { get; set; } = new ObservableCollection<Presion_arterial>();

        private Presion_arterial presion;

        public Presion_arterial Presion
        {
            get { return presion; }
            set { presion = value; }

        }

        private Control controlvista;

        public Control Controlvista
        {
            get { return controlvista; }
            set
            {
                controlvista = value;
               
            }
        }


        //Vistas
       

        //Dal
        RegistroPresion Registro = new RegistroPresion();

        public RegistrosViewModel()
            {
          


               }

        private void Nuevo()
        {
            Presion = new Presion_arterial();

        }


          
    }
}
