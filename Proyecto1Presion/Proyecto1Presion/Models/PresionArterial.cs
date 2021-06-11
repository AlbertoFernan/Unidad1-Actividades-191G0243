using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1Presion.Models
{
   
		[Table("PresionArterial")]
		public class PresionArterial
		{



			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
			public string Nombre { get; set; }

			[NotNull]
			public int Fecha { get; set; }
			[NotNull]

			public int Hora { get; set; }
			[NotNull]

			public int PresionSistolica { get; set; }
			[NotNull]

			public int PresionDiastolica { get; set; }
			[NotNull]

			public int Pulso { get; set; }








		}
	
}
