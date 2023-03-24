using Dapper;
using Domain.Entities.Entities;
using Domain.Entities.Entities.Transacciones;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.Gateway;
using Infrastructure.DrivenAdapter.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCasesTest.UnitTests
{
	public class ClienteRepositorioTest
	{
        public class ObtenerClienteTransaccionesAsyncTests
        {
            private readonly Mock<IClienteRepositorio> _mockRepositorio;

            public ObtenerClienteTransaccionesAsyncTests()
            {
                _mockRepositorio = new();
            }

            [Fact]
            public async Task ObtenerClientes()
            {
                // Arrange
                var clientes = new List<Cliente>
                {
                    new Cliente
                    {   
                        Cliente_Id = 1,
                        Nombre = "Caffir",
                        Apellido = "Torres",
                        Fecha_Nacimiento = DateTime.Now,
                        Telefono = "3024339697",
                        Correo = "Ejemplo@gmail.com",
                        Genero = "M"
                    },
                    new Cliente
                    {
                        Cliente_Id = 2,
                        Nombre = "Caffir",
                        Apellido = "Torres",
                        Fecha_Nacimiento = DateTime.Now,
                        Telefono = "3024339697",
                        Correo = "Ejemplo@gmail.com",
                        Genero = "M"
                    }

                };

                _mockRepositorio.Setup(x => x.TraerTodosLosClientesAsync()).ReturnsAsync(clientes);

                //act
                var result = await _mockRepositorio.Object.TraerTodosLosClientesAsync();

                //assert
                Assert.Equal(clientes, result);

            }

            [Fact]
            public async Task ObtenerClienteTransaccionesAsync_ReturnsExpectedResult()
            {
                // Arrange
                var expectedClientes = new List<ClienteConCuenta>
                     {
                         new ClienteConCuenta
                         {
                                Cliente_Id = 1,
                                Nombre = "John Doe",
                                Cuentas = new List<CuentaConTransaccion>
                                {
                                     new CuentaConTransaccion
                                     {
                                         Cuenta_Id = 1,
                                         Tipo_Cuenta = "Ahorros",
                                         Saldo = 1000,
                                         Transacciones = new List<TransaccionCuenta>
                                         {
                                           new TransaccionCuenta
                                           {
                                                Transaccion_Id = 1,
                                                Cuenta_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           },
                                           new TransaccionCuenta
                                           {
                                                Transaccion_Id = 2,
                                                Cuenta_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           }
                                         }
                                     }
                                }
                         },
                         new ClienteConCuenta
                         {
                            Cliente_Id = 2,
                            Nombre = "Jane Smith",
                            Cuentas = new List<CuentaConTransaccion>
                            {
                                new CuentaConTransaccion
                                {
                                    Cuenta_Id = 2,
                                    Tipo_Cuenta = "Corriente",
                                    Saldo = 500,
                                    Transacciones = new List<TransaccionCuenta>
                                    {
                                        new TransaccionCuenta
                                        {
                                            Transaccion_Id = 1,
                                            Cuenta_Id = 2,
                                            Fecha = DateTime.Now,
                                            Tipo_Transaccion = "Depósito",
                                            Descripcion = "Se hace un deposito bancario",
                                        }
                                    }
                                },
                                new CuentaConTransaccion
                                {
                                    Cuenta_Id = 3,
                                    Tipo_Cuenta = "Ahorros",
                                    Saldo = 1500,
                                    Transacciones = new List<TransaccionCuenta>
                                    {
                                        new TransaccionCuenta
                                        {
                                            Transaccion_Id = 1,
                                            Cuenta_Id = 3,
                                            Fecha = DateTime.Now,
                                            Tipo_Transaccion = "Depósito",
                                            Descripcion = "Se hace un deposito bancario"
                                        }
                                    }
                                }
                            }
                         }
                };

                _mockRepositorio.Setup(x => x.ObtenerClienteTransaccionesAsync()).ReturnsAsync(expectedClientes);

                //act
                var result = await _mockRepositorio.Object.ObtenerClienteTransaccionesAsync();

                //assert
                Assert.Equal(expectedClientes, result);
               
            }


            [Fact]
            public async Task ObtenerTarjetaTransaccionesAsync_ReturnsExpectedResult()
            {
                // Arrange
                var expectedClientes = new List<ClienteConTarjeta>
                     {
                         new ClienteConTarjeta
                         {
                                Cliente_Id = 1,
                                Nombre = "John Doe",
                                Tarjetas = new List<TarjetaConTransaccion>
                                {
                                     new TarjetaConTransaccion
                                     {
                                         Tarjeta_Id = 1,
                                         Cliente_Id = 1,
                                         Tipo_Tarjeta = "Credito",
                                         Fecha_Emision = DateTime.Now,
                                         Fecha_Vencimiento = DateTime.Now,
                                         Limite_Credito = 500000,
                                         Estado = "Activa",
                                         
                                         Transacciones = new List<TransaccionTarjeta>
                                         {
                                           new TransaccionTarjeta
                                           {
                                                Transaccion_Id = 1,
                                                Tarjeta_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un Retiro bancario",
                                           },
                                           new TransaccionTarjeta
                                           {
                                               Transaccion_Id = 2,
                                                Tarjeta_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un Retiro bancario",
                                           }
                                         }
                                     }
                                }
                         },
                         new ClienteConTarjeta
                         {
                                Cliente_Id = 2,
                                Nombre = "John Doe",
                                Tarjetas = new List<TarjetaConTransaccion>
                                {
                                     new TarjetaConTransaccion
                                     {
                                         Tarjeta_Id = 2,
                                         Cliente_Id = 2,
                                         Tipo_Tarjeta = "Credito",
                                         Fecha_Emision = DateTime.Now,
                                         Fecha_Vencimiento = DateTime.Now,
                                         Limite_Credito = 500000,
                                         Estado = "Activa",
                                         Transacciones = new List<TransaccionTarjeta>
                                         {
                                           new TransaccionTarjeta
                                           {
                                                Transaccion_Id = 1,
                                                Tarjeta_Id = 2,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un Retiro bancario",
                                           },
                                           new TransaccionTarjeta
                                           {
                                               Transaccion_Id = 2,
                                                Tarjeta_Id = 2,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un Retiro bancario",
                                           }
                                         }
                                     }
                                }
                         },
                };

                _mockRepositorio.Setup(x => x.ObtenerClienteTarjetaAsync()).ReturnsAsync(expectedClientes);

                //act
                var result = await _mockRepositorio.Object.ObtenerClienteTarjetaAsync();

                //assert
                Assert.Equal(expectedClientes, result);

            }

            [Fact]
            public async Task ObtenerClienteProductoAsync_ReturnsExpectedResult()
            {
                // Arrange
                var expectedClientes = new List<ClienteConProducto>
                     {
                         new ClienteConProducto
                         {
                                Cliente_Id = 1,
                                Nombre = "John Doe",
                                Productos = new List<ProductoConTransaccion>
                                {
                                     new ProductoConTransaccion
                                     {
                                         Producto_Id = 1,
                                         Cliente_Id = 1,
                                         Tipo_Producto = "Prestamo",
                                         Descripcion = "Prestamo de vivienda",
                                         Plazo = 12,
                                         Monto = 500000,
                                         Tasa_Interes = 2,
                                         Estado = "Activo",
                                         Transacciones = new List<TransaccionProducto>
                                         {
                                           new TransaccionProducto
                                           {
                                                Transaccion_Id = 1,
                                                Producto_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           },
                                           new TransaccionProducto
                                           {
                                                Transaccion_Id = 2,
                                                Producto_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           }
                                         }
                                     }
                                }
                         },
                          new ClienteConProducto
                         {
                                Cliente_Id = 2,
                                Nombre = "John Doe",
                                Productos = new List<ProductoConTransaccion>
                                {
                                     new ProductoConTransaccion
                                     {
                                         Producto_Id = 2,
                                         Cliente_Id = 2,
                                         Tipo_Producto = "Prestamo",
                                         Descripcion = "Prestamo de vivienda",
                                         Plazo = 12,
                                         Monto = 500000,
                                         Tasa_Interes = 2,
                                         Estado = "Activo",
                                         Transacciones = new List<TransaccionProducto>
                                         {
                                           new TransaccionProducto
                                           {
                                                Transaccion_Id = 3,
                                                Producto_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           },
                                           new TransaccionProducto
                                           {
                                                Transaccion_Id = 4,
                                                Producto_Id = 1,
                                                Fecha = DateTime.Now,
                                                Tipo_Transaccion = "Depósito",
                                                Descripcion = "Se hace un deposito bancario",
                                           }
                                         }
                                     }
                                }
                         },
                };

                _mockRepositorio.Setup(x => x.ObtenerClienteProductoAsync()).ReturnsAsync(expectedClientes);

                //act
                var result = await _mockRepositorio.Object.ObtenerClienteProductoAsync();

                //assert
                Assert.Equal(expectedClientes, result);

            }


        }
    }
}
