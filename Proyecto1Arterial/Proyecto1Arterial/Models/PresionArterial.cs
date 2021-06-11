using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1Arterial.Models
{
	[Table("PresionArterial")]
	public class PresionArterial
    {
		

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Nombre { get; set; } = "";

		[NotNull]
		public DateTime Fecha { get; set; } = DateTime.Now.Date;
		[NotNull]

		public TimeSpan Hora { get; set; } = DateTime.Now.TimeOfDay;
		[NotNull]

		public int PresionSistolica { get; set; }
		[NotNull]

		public int PresionDiastolica { get; set; }
		[NotNull]

		public int Pulso { get; set; }
	}
}
