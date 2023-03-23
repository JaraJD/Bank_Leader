﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Commands
{
    public class InsertarNuevoProducto
    {
		public int Id_Cliente { get; set; }
		public string Tipo_Producto { get; set; }
        public string Descripcion { get; set; }
        public int Plazo { get; set; }
        public decimal Monto { get; set; }
        public decimal Tasa_Interes { get; set; }
        public string Estado { get; set; }
    }
}
