using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoJardineria191G0243
{
    public class PlantaJugador:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int tamaño;

        public int Tamaño
        {
            get { return tamaño; }
            set { tamaño = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tamaño))); }
        }




     

        private decimal dinero;
        public decimal Dinero
        {
            get { return dinero; }
            set { dinero = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dinero))); }
        }

        


    }
}
