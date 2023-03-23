using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
	public class CuentaConTransaccion
	{
		public int Cuenta_Id { get; set; }
		public int Cliente_Id { get; set; }
		public string Tipo_Cuenta { get; set; }
		public decimal Saldo { get; set; }
		public DateTime Fecha_Apertura { get; set; }
		public DateTime Fecha_Cierre { get; set; }
		public decimal Tasa_Interes { get; set; }
		public string Estado { get; set; }
		public List<Transaccion> Transacciones { get; set;}
	}
}
