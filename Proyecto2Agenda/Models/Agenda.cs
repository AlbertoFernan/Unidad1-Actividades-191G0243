﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto2Agenda.Models
{
    public class Agenda
    {
        SQLiteConnection Conexionb;

        public Agenda()
        {
            Conexionb = new SQLiteConnection("baseagenda.db");
            Conexionb.CreateTable<Cita>(); //Crear la tabla si no existe
        }

        public List<string> ListaErrores { get; set; }
        public bool Insert(Cita c)
        {
            ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(c.Nombre))
            {

                ListaErrores.Add("No especifico el nombre");
            }
            if (string.IsNullOrWhiteSpace(c.Negocio))
            {

                ListaErrores.Add("No especifico el asunto");
            }
            if (c.Fecha < DateTime.Now)
            {

                ListaErrores.Add("Inserte una fecha futura");
            }
            if (Conexionb.Table<Cita>().Any(x => x.Nombre.ToUpper() == c.Nombre.ToUpper()) && Conexionb.Table<Cita>().Any(x => x.Fecha == c.Fecha))
            {
                ListaErrores.Add("Ya existe un registro en esta fecha con este nombre");
            }

            if (ListaErrores.Count > 0) return false;

            Conexionb.Insert(c);

            return true;
        }
        public IEnumerable<Cita> GetAll()
        {
            return Conexionb.Table<Cita>().OrderBy(x => x.Fecha);
        }

        public Cita Get(int Id)
        {
            return Conexionb.Get<Cita>(Id);
        }
        public bool Update(Cita Copia)
        {
            ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(Copia.Nombre))
            {

                ListaErrores.Add("No especifico el nombre");
            }
            if (string.IsNullOrWhiteSpace(Copia.Negocio))
            {

                ListaErrores.Add("No especifico el asunto");
            }
         

            if (ListaErrores.Count > 0) return false;

            var Citabd = Get(Copia.Id);

            Citabd.Nombre = Copia.Nombre;
            Citabd.Fecha = Copia.Fecha;
            Citabd.Negocio = Copia.Negocio;



            Conexionb.Update(Citabd);

            return true;


        }
        public void Delete(Cita p)
        {
            Conexionb.Delete(p);
        }
    }
}
