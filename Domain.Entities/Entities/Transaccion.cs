using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{

    public class Transaccion
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int Id_Cuenta { get; set; }
        
        [Required]
        public int Id_Tarjeta { get; set; }
        
        [Required]
        public int Id_Producto { get; set; }
        
        [Required]
        public int Id_Cliente { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        
        [Required]
        public string Hora { get; set; }
        
        [Required]
        public string Tipo_Transaccion { get; set; }
        
        [Required]
        public string Descripcion { get; set; }
        
        [Required]
        public decimal Monto { get; set; }
        
        [Required]
        public decimal Saldo_Final { get; set; }
        
        [Required]
        public decimal Saldo_Anterior { get; set;}
    }
}
