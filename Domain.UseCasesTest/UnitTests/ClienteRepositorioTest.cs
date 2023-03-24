using Dapper;
using Domain.Entities.Commands;
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
            public async Task Insertar_Cliente()
            {
                //Arrange

                var insertarCliente = new InsertarNuevoCliente
                {
                    Nombre = "Juan",
                    Apellido = "Jaramillo",
                    Fecha_Nacimiento = DateTime.Now,
                    Telefono = "555-4859",
                    Correo = "juan.jaramillo@mail.com",
                    Genero = "M"
                };

                var cliente = new InsertarNuevoCliente
                {
                    Nombre = "Juan",
                    Apellido = "Jaramillo",
                    Fecha_Nacimiento = DateTime.Now,
                    Telefono = "555-4859",
                    Correo = "juan.jaramillo@mail.com",
                    Genero = "M"
                };

                _mockRepositorio.Setup(repo => repo.InsertarClienteAsync(insertarCliente))
                    .ReturnsAsync(cliente);

                //Act

                var res = await _mockRepositorio.Object.InsertarClienteAsync(insertarCliente);

                //Assert
                Assert.Equal(cliente, res);
            }

            [Fact]
            public async Task Get_Cliente_con_Activos()
            {
                //arrange
                var test = new ClienteConActivos
                {
                    Cliente_Id = 1,
                    Nombre = "Juan",
                    Apellido = "Jaramillo",
                    Fecha_Nacimiento = DateTime.Now,
                    Telefono = "555-6857",
                    Correo = "juan.jara@correo.com",
                    Genero = "M",
                    Cuentas = new List<Cuenta>
                {
                    new Cuenta
                    {
                      Cuenta_Id = 1,
                      Cliente_Id = 1,
                      Tipo_Cuenta = "Ahorros",
                      Saldo = 200000,
                      Fecha_Apertura = DateTime.Now,
                      Fecha_Cierre = DateTime.Now,
                      Tasa_Interes = 0,
                      Estado = "Activa"
                    },
                    new Cuenta
                    {
                      Cuenta_Id = 2,
                      Cliente_Id = 1,
                      Tipo_Cuenta = "Corriente",
                      Saldo = 5586888,
                      Fecha_Apertura = DateTime.Now,
                      Fecha_Cierre = DateTime.Now,
                      Tasa_Interes = 2,
                      Estado = "Activa"
                    }

                },

                    Productos = new List<Producto>
                {
                    new Producto
                    {
                        Producto_Id = 1,
                        Cliente_Id = 1,
                        Tipo_Producto = "Credito",
                        Descripcion = "Libre destino",
                        Plazo = 36,
                        Monto = 5000000,
                        Tasa_Interes = 2,
                        Estado = "Activa"
                    },
                    new Producto
                    {
                        Producto_Id = 2,
                        Cliente_Id = 1,
                        Tipo_Producto = "Credito",
                        Descripcion = "Hipotecario",
                        Plazo = 120,
                        Monto = 36000000,
                        Tasa_Interes = 3,
                        Estado = "Activa"
                    }

                },
                    Tarjetas = new List<Tarjeta>
                {
                      new Tarjeta
                      {
                        Tarjeta_Id = 1,
                        Cliente_Id = 1,
                        Tipo_Tarjeta = "Ahorros",
                        Fecha_Emision = DateTime.Now,
                        Fecha_Vencimiento = DateTime.Now,
                        Limite_Credito = 5000000,
                        Estado = "Activo"
                      },
                      new Tarjeta
                      {
                        Tarjeta_Id = 2,
                        Cliente_Id = 1,
                        Tipo_Tarjeta = "Credito",
                        Fecha_Emision = DateTime.Now,
                        Fecha_Vencimiento = DateTime.Now,
                        Limite_Credito = 4800000,
                        Estado = "Activo"
                      }


                }
                };
                _mockRepositorio.Setup(x => x.ObtenerClienteActivosAsync(1)).ReturnsAsync(test);
                //act
                var result = await _mockRepositorio.Object.ObtenerClienteActivosAsync(1);
                //assert
                Assert.Equal(test, result);
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

            [Fact]
            public async Task ObtenerClienteID()
            {
                // Arrange
                var cliente = new Cliente
                {
                    Cliente_Id = 1,
                    Nombre = "Caffir",
                    Apellido = "Torres",
                    Fecha_Nacimiento = DateTime.Now,
                    Telefono = "3024339697",
                    Correo = "Ejemplo@gmail.com",
                    Genero = "M"
                };
      

                _mockRepositorio.Setup(x => x.ObtenerClientePorIdAsync(1)).ReturnsAsync(cliente);

                //act
                var result = await _mockRepositorio.Object.ObtenerClientePorIdAsync(1);

                //assert
                Assert.Equal(cliente, result);

            }


        }
    }
}
