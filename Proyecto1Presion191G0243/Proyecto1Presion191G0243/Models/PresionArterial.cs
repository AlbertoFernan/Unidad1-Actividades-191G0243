using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1Presion191G0243.Models
{
	[Table("Presion arterial")]
	public class PresionArterial
    {
		
	

			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
			public string Nombre { get; set; }

			[NotNull]
			public DateTime Fecha { get; set; }
			[NotNull]

			public DateTime Hora { get; set; }
			[NotNull]

			public int PresionSistolica { get; set; }
			[NotNull]

			public int PresionDiastolica { get; set; }
			[NotNull]

			public int Pulso { get; set; }







		
	}
}
