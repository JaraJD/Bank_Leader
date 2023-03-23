using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.UseCase
{
    public class ProductoCasoDeUso : IProductoCasoDeUso
    {
        private readonly IProductoRepositorio productoRepositorio;

        public ProductoCasoDeUso(IProductoRepositorio productoRepositorio)
        {
            this.productoRepositorio = productoRepositorio;
        }

        public async Task<Producto> AgregarProducto(Producto producto)
        {
            return await productoRepositorio.InsertarProductoAsync(producto);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await productoRepositorio.TraerTodosLosProductos();
        }
    }
}
