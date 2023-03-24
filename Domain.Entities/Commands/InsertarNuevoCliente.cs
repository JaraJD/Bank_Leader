using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Commands
{
    public class InsertarNuevoCliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
    }
}
