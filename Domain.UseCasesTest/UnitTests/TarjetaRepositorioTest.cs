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
    public class TarjetaRepositorioTest
    {
        private readonly Mock<ITarjetaRepositorio> _mockTarjetaRepositorio;

        public TarjetaRepositorioTest()
        {
            _mockTarjetaRepositorio = new();
        }

        [Fact]
        public async Task InsertarTarjetaAsync()
        {
            // Arrange
            var insertarNuevaTarjeta = new InsertarNuevaTarjeta
            {
                Cliente_Id = 1,
                Tipo_Tarjeta = "Debito",
                Fecha_Emision = DateTime.Today,
                Fecha_Vencimiento = DateTime.Today.AddYears(5),
                Limite_Credito = 1000,
                Estado = "Activo"
            };

            var tarjeta = new InsertarNuevaTarjeta()
            {
                Cliente_Id = 1,
                Tipo_Tarjeta = "Debito",
                Fecha_Emision = DateTime.Today,
                Fecha_Vencimiento = DateTime.Today.AddYears(5),
                Limite_Credito = 1000,
                Estado = "Activo"
            };

            _mockTarjetaRepositorio.Setup(x => x.InsertarTarjetaAsync(insertarNuevaTarjeta)).ReturnsAsync(tarjeta);

            // Act
            var result = await _mockTarjetaRepositorio.Object.InsertarTarjetaAsync(insertarNuevaTarjeta);

            // Assert
            Assert.Equal(tarjeta, result);
        }

        [Fact]
        public async Task TraerTodasLasTarjetas()
        {
            //Arrange
            var tarjetas = new List<Tarjeta>
            {
                new Tarjeta
                {
                    Tarjeta_Id = 1,
                    Cliente_Id = 1,
                    Tipo_Tarjeta = "Debito",
                    Fecha_Emision = DateTime.Today,
                    Fecha_Vencimiento = DateTime.Today.AddYears(5),
                    Limite_Credito = 1000,
                    Estado = "Activo"
                },

                new Tarjeta
                {
                    Tarjeta_Id = 2,
                    Cliente_Id = 1,
                    Tipo_Tarjeta = "Debito",
                    Fecha_Emision = DateTime.Today,
                    Fecha_Vencimiento = DateTime.Today.AddYears(2),
                    Limite_Credito = 1000,
                    Estado = "Activo"
                }
            };

            _mockTarjetaRepositorio.Setup(x => x.TraerTodasLasTarjetas()).ReturnsAsync(tarjetas);

            //Act
            var resultado = await _mockTarjetaRepositorio.Object.TraerTodasLasTarjetas();

            //Assert
            Assert.Equal(tarjetas, resultado);
        }
    }
}
