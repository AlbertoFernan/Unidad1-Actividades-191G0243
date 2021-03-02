using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Actividad_5
{
    public class SimuladorInversion:INotifyPropertyChanged
    {
        private decimal monto;

        private short plazo;

        public decimal MontoInvertir

        {

            get { return monto; }

            set { monto = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }

        }

        public short PlazoAnual

        {

            get { return plazo; }

            set { plazo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }

        }

        public decimal Total

        {

            get

            {

                decimal CantidadTotal = monto;

                for (int i = 0; i != plazo; i++)

                {

                    CantidadTotal += (CantidadTotal * 0.07m);

                    CantidadTotal = (CantidadTotal > 30000) ? CantidadTotal - (CantidadTotal * 0.02m) : CantidadTotal;

                }

                return CantidadTotal;

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
