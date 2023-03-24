using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Moq;

namespace Domain.UseCasesTest.UnitTests
{
    public class ProductoRepositorioTest
    {
        private readonly Mock<IProductoRepositorio> _mockProductoRepositorio;
        public ProductoRepositorioTest()
        {
            _mockProductoRepositorio = new();
        }

        [Fact]
        public async Task InsertarProductoAsync()
        {
            //Arrange
            var insertarProducto = new InsertarNuevoProducto
            {
                Cliente_Id = 1,
                Tipo_Producto = "X",
                Descripcion = "X",
                Plazo = 6,
                Monto = 10,
                Tasa_Interes = 1,
                Estado = "Activo"
            };

            var producto = new InsertarNuevoProducto
            {
                Cliente_Id = 1,
                Tipo_Producto = "X",
                Descripcion = "X",
                Plazo = 6,
                Monto = 10,
                Tasa_Interes = 1,
                Estado = "Activo"
            };

            _mockProductoRepositorio.Setup(x => x.InsertarProductoAsync(insertarProducto)).ReturnsAsync(producto);

            //Act
            var resultado = await _mockProductoRepositorio.Object.InsertarProductoAsync(insertarProducto);

            //Assert
            Assert.Equal(producto, resultado);
        }

        [Fact]
        public async Task TraerTodosLosProductos()
        {
            //Arrange
            var productos = new List<Producto>
            {
                new Producto
                {
                    Producto_Id =1,
                    Cliente_Id = 1,
                    Tipo_Producto = "X",
                    Descripcion = "X",
                    Plazo = 6,
                    Monto = 10,
                    Tasa_Interes = 1,
                    Estado = "Activo"
                },
                new Producto
                {
                    Producto_Id =2,
                    Cliente_Id = 1,
                    Tipo_Producto = "X",
                    Descripcion = "X",
                    Plazo = 6,
                    Monto = 10,
                    Tasa_Interes = 1,
                    Estado = "Activo"
                }
            };

            _mockProductoRepositorio.Setup(x => x.TraerTodosLosProductos()).ReturnsAsync(productos);

            //Act
            var resultado = await _mockProductoRepositorio.Object.TraerTodosLosProductos();

            //Assert
            Assert.Equal(productos, resultado);
        }

    }
}
