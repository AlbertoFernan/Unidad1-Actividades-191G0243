using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto2Agenda.Models
{
    
		[Table("Citas")]
		public class Cita
		{

			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
		[NotNull]
		public string Nombre { get; set; } = "";

			[NotNull]
			public DateTime Fecha { get; set; } = DateTime.Now.Date;


		[NotNull]
		public string Negocio { get; set; } = "";











		}
	
}
