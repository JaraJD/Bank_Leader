using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Commands
{
    public class InsertarNuevaCuenta
    {
        public int Id_Cliente { get; set; }
        public string Tipo_Cuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Fecha_Apertura { get; set; }
        public DateTime Fecha_Cierre { get; set; }
        public decimal Tasa_Interes { get; set; }
        public string Estado { get; set; }
    }
}
