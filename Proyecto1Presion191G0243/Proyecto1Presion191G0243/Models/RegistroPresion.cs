using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Proyecto1Presion191G0243.Models
{
    public class RegistroPresion
    {
        public SQLiteConnection Conexion { get; set; }
        public string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/PacientesB.db";

        public RegistroPresion()
        {
            VerificarBD();
            Conexion = new SQLiteConnection(ruta);

         

        }
        private void VerificarBD()
        {
            if (!File.Exists(ruta))
            {
                Assembly a = Assembly.GetExecutingAssembly();
                var stream = a.GetManifestResourceStream("Proyecto1Presion191G0243.Data.PacientesB.db");
                var archivo = File.Create(ruta);
                stream.CopyTo(archivo);
                stream.Close();
                archivo.Close();
            }
        }

        public List<string> ListaErrores { get; set; }
        public bool Insert(PresionArterial p)
        {
            ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(p.Nombre))
            {

                ListaErrores.Add("No especifico el nombre");
            }
            if (p.PresionDiastolica == 0)
            {
                ListaErrores.Add("No inserto la presion diastolica");
            }
            if (p.PresionSistolica == 0)
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
        public IEnumerable<PresionArterial> GetAll()
        {
            return Conexion.Table<PresionArterial>();
        }

        public PresionArterial Get(int Id)
        {
            return Conexion.Get<PresionArterial>(Id);
        }
        public bool Update(PresionArterial Copia)
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
            Presionbd.Fecha = Copia.Fecha;
            Presionbd.Hora = Copia.Hora;
            Presionbd.PresionDiastolica = Copia.PresionDiastolica;
            Presionbd.PresionSistolica = Copia.PresionSistolica;
            Presionbd.Pulso = Copia.Pulso;

            Conexion.Update(Presionbd);

            return true;


        }
        public void Delete(PresionArterial p)
        {
            Conexion.Delete(p);
        }
    }
}
