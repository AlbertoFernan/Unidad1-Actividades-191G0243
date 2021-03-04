using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Actividad_7
{
    public class PastoSintetico:INotifyPropertyChanged
    {
        private int altura = 5;
        private float largo, ancho;
        private bool instalacion;

        public event PropertyChangedEventHandler PropertyChanged;

        public float LargoTerreno
        {
            get { return largo; }
            set
            {

                if (value > 0)
                {
                    largo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
                else
                {
                    largo = 0;
                }
            }
        }
        public float AnchoTerreno
        {
            get { return ancho; }
            set
            {

                if (value > 0)
                {
                    ancho = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
                else
                {
                    ancho = 0;
                }
            }
        }



        public bool RequiereInstalacion
        {
            get { return instalacion; }
            set { instalacion = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }

        public int AlturaCesped
        {
            get { return altura; }
            set
            {

                if (value < 5 || value > 65 || value % 5 != 0)
                {
                    altura = 5; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
                else
                {
                    altura = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));


                }
            }
        }


        public float MetrosCuadrados
        {
            get
            {
                return largo * ancho;
            }
        }

        public decimal CostoMetroCuadrado
        {
            get { return 38m * (AlturaCesped / 10m); }
        }

        public decimal Subtotal
        {
            get { return CostoMetroCuadrado * (decimal)MetrosCuadrados; }
        }



        public decimal CostoInstalacion
        {
            get { return RequiereInstalacion ? (decimal)MetrosCuadrados * 40 : 0; }
        }

        public decimal Descuento
        {
            get { return RequiereInstalacion ? Subtotal * .12m : 0; }
        }

        public decimal Total
        {
            get { return Subtotal + CostoInstalacion - Descuento; }
        }
    }
}
