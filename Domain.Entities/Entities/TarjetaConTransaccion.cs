using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
	public class TarjetaConTransaccion
	{
		public int Tarjeta_Id { get; set; }
		public int Cliente_Id { get; set; }
		public string Tipo_Tarjeta { get; set; }
		public DateTime Fecha_Emision { get; set; }
		public DateTime Fecha_Vencimiento { get; set; }
		public decimal Limite_Credito { get; set; }
		public string Estado { get; set; }
		public List<Transaccion> Transacciones { get; set;}
	}
}
