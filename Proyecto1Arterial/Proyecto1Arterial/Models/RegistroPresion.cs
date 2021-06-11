using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Proyecto1Arterial.Models
{
    public class RegistroPresion
    {
        public SQLiteConnection Conexion { get; set; }
        public string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Pacientesv3.db3";

        public RegistroPresion()
        {
            VerificarBD();
            Conexion = new SQLiteConnection(ruta);





        }
        private void VerificarBD()
        {
            if (!System.IO.File.Exists(ruta)) //Si el archivo alumnos.db3 no existe en el dispositivo
            {
                Assembly ensamblado = Assembly.GetExecutingAssembly();
                Stream s = ensamblado.GetManifestResourceStream("Proyecto1Arterial.Data.Pacientesv3.db3");


                FileStream archivo = File.Create(ruta);
                s.CopyTo(archivo);
                archivo.Close();
                s.Close();
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
            if (p.PresionDiastolica == 0 )
            {
                ListaErrores.Add("No inserto bien la presion diastolica (No use letras)");
            }
            if (p.PresionSistolica == 0)
            {
                ListaErrores.Add("No inserto bien la presion sistolica (No use letras)");
            }
            if (p.Pulso == 0)
            {
                ListaErrores.Add("No inserto bien el pulso (No use letras)");
            }
            if (Conexion.Table<PresionArterial>().Any(x => x.Nombre.ToUpper() == p.Nombre.ToUpper()) && Conexion.Table<PresionArterial>().Any(x => x.Fecha == p.Fecha) && Conexion.Table<PresionArterial>().Any(x => x.Hora == p.Hora))
            {
                ListaErrores.Add("Ya existe un registro a la misma hora y fecha con este nombre");
            }
     
            if (ListaErrores.Count > 0) return false;

            Conexion.Insert(p);

            return true;
        }
        public IEnumerable<PresionArterial> GetAll()
        {
            return Conexion.Table<PresionArterial>().OrderBy(x => x.Nombre).ThenBy(x => x.Fecha);
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
                ListaErrores.Add("No inserto bien la presion diastolica (No use letras)");
            }
            if (Copia.PresionSistolica == 0)
            {
                ListaErrores.Add("No inserto bien la presion sistolica (No use letras)");
            }
            if (Copia.Pulso == 0)
            {
                ListaErrores.Add("No inserto bien el pulso (No use letras)");
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
