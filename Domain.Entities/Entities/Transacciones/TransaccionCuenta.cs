using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities.Transacciones
{
	public class TransaccionCuenta
	{
		[Required]
		public int Cuenta_Id { get; set; }

		[Required]
		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime Fecha { get; set; }

		[Required]
		public string Tipo_Transaccion { get; set; }

		[Required]
		public string Descripcion { get; set; }
	}
}
