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
    public class CuentaRepositorioTest
    {
        private readonly Mock<ICuentaRepositorio> _mockCuentaRepositorio;

        public CuentaRepositorioTest()
        {
            _mockCuentaRepositorio = new Mock<ICuentaRepositorio>();
        }

        [Fact]
        public async Task InsertarCuentaAsync()
        {
            //Arrange
            var insertarNuevaCuenta = new InsertarNuevaCuenta
            {
                Cliente_Id = 1,
                Tipo_Cuenta = "Ahorro",
                Saldo = 1000,
                Fecha_Apertura = DateTime.Today,
                Fecha_Cierre = DateTime.Today,
                Tasa_Interes = 1,
                Estado = "Activo"
            };

            var cuenta = new InsertarNuevaCuenta
            {
                Cliente_Id = 1,
                Tipo_Cuenta = "Ahorro",
                Saldo = 1000,
                Fecha_Apertura = DateTime.Today,
                Fecha_Cierre = DateTime.Today,
                Tasa_Interes = 1,
                Estado = "Activo"
            };

            _mockCuentaRepositorio.Setup(x => x.InsertarCuentaAsync(insertarNuevaCuenta)).ReturnsAsync(cuenta);

            //Act
            var cuentaResult = await _mockCuentaRepositorio.Object.InsertarCuentaAsync(insertarNuevaCuenta);

            //Assert
            Assert.Equal(cuenta, cuentaResult);
        }

        [Fact]
        public async Task TraerTodasLasCuentas()
        {
            //Arrange
            var cuentas = new List<Cuenta>
            {
                new Cuenta
                {
                    Cuenta_Id = 1,
                    Cliente_Id = 1,
                    Tipo_Cuenta = "Ahorro",
                    Saldo = 1000,
                    Fecha_Apertura = DateTime.Today,
                    Fecha_Cierre = DateTime.Today,
                    Tasa_Interes = 1,
                    Estado = "Activo"
                },
                new Cuenta
                {
                    Cuenta_Id = 2,
                    Cliente_Id = 2,
                    Tipo_Cuenta = "Ahorro",
                    Saldo = 1000,
                    Fecha_Apertura = DateTime.Today,
                    Fecha_Cierre = DateTime.Today,
                    Tasa_Interes = 1,
                    Estado = "Activo"
                }
            };

            _mockCuentaRepositorio.Setup(x => x.TraerTodasLasCuentas()).ReturnsAsync(cuentas);

            //Act
            var cuentasResult = await _mockCuentaRepositorio.Object.TraerTodasLasCuentas();

            //Assert
            Assert.Equal(cuentas, cuentasResult);
        }
    }
}
