using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.Gateway;

namespace Infrastructure.DrivenAdapter.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "Cliente";

        public ClienteRepositorio(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }


        public async Task<InsertarNuevoCliente> InsertarClienteAsync(InsertarNuevoCliente cliente)
        {
            Guard.Against.Null(cliente, nameof(cliente));
            Guard.Against.NullOrEmpty(cliente.Nombre, nameof(cliente.Nombre));
            Guard.Against.NullOrEmpty(cliente.Apellido, nameof(cliente.Apellido));
            Guard.Against.NullOrEmpty(cliente.Fecha_Nacimiento.ToString(), nameof(cliente.Fecha_Nacimiento));
            Guard.Against.NullOrEmpty(cliente.Telefono, nameof(cliente.Telefono));
            Guard.Against.NullOrEmpty(cliente.Correo, nameof(cliente.Correo));
            Guard.Against.NullOrEmpty(cliente.Genero, nameof(cliente.Genero));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarNuevoCliente = new
            {
                nombre = cliente.Nombre,
                apellido = cliente.Apellido,
                fecha_nacimiento = cliente.Fecha_Nacimiento,
                telefono = cliente.Telefono,
                correo = cliente.Correo,
                genero = cliente.Genero
            };
            string query = $"INSERT INTO {nombreTabla} (nombre, apellido, fecha_nacimiento, telefono, correo, genero) VALUES (@nombre, @apellido, @fecha_nacimiento, @telefono, @correo, @genero)";
            var resultado = await connection.ExecuteAsync(query, insertarNuevoCliente);
            return cliente;
        }

		public async Task<List<Cliente>> TraerTodosLosClientesAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();
			Guard.Against.Null(connection, nameof(connection));

			string query = $"SELECT * FROM {nombreTabla}";
			var resultado = await connection.QueryAsync<Cliente>(query);
			Guard.Against.Null(resultado, nameof(resultado));

			connection.Close();
			return resultado.ToList();
		}

		public async Task<Cliente> ObtenerClientePorIdAsync(int idCliente)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();
			string sqlQuery = $"SELECT * FROM {nombreTabla} WHERE cliente_id = @idCliente";
			var result = await connection.QuerySingleAsync<Cliente>(sqlQuery, new { idCliente });
			connection.Close();
			return result;
		}

		public async Task<List<ClienteConProducto>> ObtenerClienteProductoAsync()
		{
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var sql =
                    @"SELECT DISTINCT C.*, A.*, T.*
					FROM Cliente C
					INNER JOIN Producto A ON A.cliente_id = C.cliente_id
					INNER JOIN Transaccion T ON T.producto_id = A.producto_id";

            var clientesDic = new Dictionary<int, ClienteConProducto>();
            var clientes = await connection.QueryAsync<ClienteConProducto, ProductoConTransaccion, Transaccion, ClienteConProducto>(sql,
                (cliente, producto, transaccion) =>
                {
                    if (!clientesDic.TryGetValue(cliente.Cliente_Id, out ClienteConProducto clienteEntry))
                    {
                        clienteEntry = cliente;
                        clienteEntry.Productos = new List<ProductoConTransaccion>();
                        clientesDic.Add(clienteEntry.Cliente_Id, clienteEntry);
                    }

                    if (clienteEntry.Productos != null && !clienteEntry.Productos.Any(c => c.Producto_Id == producto.Producto_Id))
                    {
                        producto.Transacciones = new List<Transaccion>();
                        producto.Transacciones.Add(transaccion);
                        clienteEntry.Productos.Add(producto);
                    }
                    else
                    {
                        var cuentaEntry = clienteEntry.Productos.FirstOrDefault(c => c.Producto_Id == producto.Producto_Id);
                        if (cuentaEntry.Transacciones != null && !cuentaEntry.Transacciones.Any(t => t.Transaccion_Id == transaccion.Transaccion_Id))
                        {
                            cuentaEntry.Transacciones.Add(transaccion);
                        }
                    }

                    return clienteEntry;
                },
                splitOn: "producto_Id, transaccion_Id");

            connection.Close();
            return clientesDic.Values.ToList();
        }

		public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
		{
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var sql =
                    @"SELECT DISTINCT C.*, A.*, T.*
					FROM Cliente C
					INNER JOIN Tarjeta A ON A.cliente_id = C.cliente_id
					INNER JOIN Transaccion T ON T.tarjeta_id = A.tarjeta_id";

            var clientesDic = new Dictionary<int, ClienteConTarjeta>();
            var clientes = await connection.QueryAsync<ClienteConTarjeta, TarjetaConTransaccion, Transaccion, ClienteConTarjeta>(sql,
                (cliente, tarjeta, transaccion) =>
                {
                    if (!clientesDic.TryGetValue(cliente.Cliente_Id, out ClienteConTarjeta clienteEntry))
                    {
                        clienteEntry = cliente;
                        clienteEntry.Tarjetas = new List<TarjetaConTransaccion>();
                        clientesDic.Add(clienteEntry.Cliente_Id, clienteEntry);
                    }

                    if (clienteEntry.Tarjetas != null && !clienteEntry.Tarjetas.Any(c => c.Tarjeta_Id == tarjeta.Tarjeta_Id))
                    {
                        tarjeta.Transacciones = new List<Transaccion>();
                        tarjeta.Transacciones.Add(transaccion);
                        clienteEntry.Tarjetas.Add(tarjeta);
                    }
                    else
                    {
                        var cuentaEntry = clienteEntry.Tarjetas.FirstOrDefault(c => c.Tarjeta_Id == tarjeta.Tarjeta_Id);
                        if (cuentaEntry.Transacciones != null && !cuentaEntry.Transacciones.Any(t => t.Transaccion_Id == transaccion.Transaccion_Id))
                        {
                            cuentaEntry.Transacciones.Add(transaccion);
                        }
                    }

                    return clienteEntry;
                },
                splitOn: "tarjeta_Id, transaccion_Id");

            connection.Close();
            return clientesDic.Values.ToList();
        }

		public async Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
		{
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var sql = 
					@"SELECT DISTINCT C.*, A.*, T.*
					FROM Cliente C
					INNER JOIN Cuenta A ON A.cliente_id = C.cliente_id
					INNER JOIN Transaccion T ON T.cuenta_id = A.cuenta_id";

            var clientesDic = new Dictionary<int, ClienteConCuenta>();
            var clientes = await connection.QueryAsync<ClienteConCuenta, CuentaConTransaccion, Transaccion, ClienteConCuenta>(sql,
                (cliente, cuenta, transaccion) =>
                {
                    if (!clientesDic.TryGetValue(cliente.Cliente_Id, out ClienteConCuenta clienteEntry))
                    {
                        clienteEntry = cliente;
                        clienteEntry.Cuentas = new List<CuentaConTransaccion>();
                        clientesDic.Add(clienteEntry.Cliente_Id, clienteEntry);
                    }

                    if (clienteEntry.Cuentas != null && !clienteEntry.Cuentas.Any(c => c.Cuenta_Id == cuenta.Cuenta_Id))
                    {
                        cuenta.Transacciones = new List<Transaccion>();
                        cuenta.Transacciones.Add(transaccion);
                        clienteEntry.Cuentas.Add(cuenta);
                    }
                    else
                    {
                        var cuentaEntry = clienteEntry.Cuentas.FirstOrDefault(c => c.Cuenta_Id == cuenta.Cuenta_Id);
                        if (cuentaEntry.Transacciones != null && !cuentaEntry.Transacciones.Any(t => t.Transaccion_Id == transaccion.Transaccion_Id))
                        {
                            cuentaEntry.Transacciones.Add(transaccion);
                        }
                    }

                    return clienteEntry;
                },
                splitOn: "cuenta_Id, transaccion_Id");

            connection.Close();
            return clientesDic.Values.ToList();
        }
	}
}
