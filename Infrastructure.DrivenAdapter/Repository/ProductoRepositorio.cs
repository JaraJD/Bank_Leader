using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repository
{
	public class ProductoRepositorio : IProductoRepositorio
	{
		public Task<InsertarNuevoProducto> InsertarProductoAsync(InsertarNuevoProducto producto)
		{
			throw new NotImplementedException();
		}

		public Task<List<Producto>> TraerTodosLosProductos()
		{
			throw new NotImplementedException();
		}
	}
}
