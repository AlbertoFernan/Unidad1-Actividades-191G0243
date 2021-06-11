using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Recetario
{
    [Serializable]
    public class Receta
    {
        public string Nombre { get; set; }

        public string Ingredientes { get; set; }
        public string Procedimiento { get; set; }
        public string Imagen { get; set; }
    }
}
