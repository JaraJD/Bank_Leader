using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public string Tipo_Tarjeta { get; set; }
        public DateTime Fecha_Emision { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public decimal Limite_Credito { get; set; }
        public string Estado { get; set; }
    }
}
