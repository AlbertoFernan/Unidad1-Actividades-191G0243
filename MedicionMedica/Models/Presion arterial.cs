using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicionMedica.Models
{
	[Table("Presionarterial")]
	public class Presion_arterial
	{

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Nombre { get; set; }

		[NotNull]
		public DateTime Fecha { get; set; }
		[NotNull]

		public int Hora { get; set; }
		[NotNull]

		public float PresionSistolica { get; set; }
		[NotNull]

		public float PresionDiastolica { get; set; }
		[NotNull]

		public float Pulso { get; set; }
		






	}






}
