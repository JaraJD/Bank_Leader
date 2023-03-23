using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    class Empleado
    {
        public int Id { get; set; }
        public int Id_Sucursal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Tarjeta_Acceso { get; set; }
        public string Estado { get; set; }
    }
}
