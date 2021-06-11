using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicionMedica.Models
{
    public class RegistroPresion
    {
        public SQLiteConnection Conexion { get; set; }

        public RegistroPresion()
            {
        Conexion = new SQLiteConnection("./Data/PacientesB.db");

        }

        public List<string> ListaErrores { get; set; }
        public bool Insert(Presion_arterial p)
        {
            ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(p.Nombre)) 
            {

                ListaErrores.Add("No especifico el nombre");
            }
            if ( p.PresionDiastolica==0)
            {
                ListaErrores.Add("No inserto la presion diastolica");
            }
            if (p.PresionSistolica== 0)
            {
                ListaErrores.Add("No inserto la presion sistolica");
            }
            if (p.Pulso == 0)
            {
                ListaErrores.Add("No inserto el pulso");
            }
            if (ListaErrores.Count > 0) return false;

            Conexion.Insert(p);

            return true;
        }
        public IEnumerable<Presion_arterial> GetAll() 
        {
            return Conexion.Table<Presion_arterial>();
        }

        public Presion_arterial Get(int Id)
        {
            return Conexion.Get<Presion_arterial>(Id);
        }
        public bool Update(Presion_arterial Copia) 
        {
            ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(Copia.Nombre))
            {

                ListaErrores.Add("No especifico el nombre");
            }
            if (Copia.PresionDiastolica == 0)
            {
                ListaErrores.Add("No inserto la presion diastolica");
            }
            if (Copia.PresionSistolica == 0)
            {
                ListaErrores.Add("No inserto la presion sistolica");
            }
            if (Copia.Pulso == 0)
            {
                ListaErrores.Add("No inserto el pulso");
            }
            if (ListaErrores.Count > 0) return false;

            var Presionbd = Get(Copia.Id);

            Presionbd.Nombre = Copia.Nombre;
           
            Presionbd.Hora = Copia.Hora;
            Presionbd.PresionDiastolica = Copia.PresionDiastolica;
            Presionbd.PresionSistolica = Copia.PresionSistolica;
            Presionbd.Pulso = Copia.Pulso;

            Conexion.Update(Presionbd);

            return true;


        }
        public void Delete(Presion_arterial p)
        {
            Conexion.Delete(p);
                }
        
    }
}
