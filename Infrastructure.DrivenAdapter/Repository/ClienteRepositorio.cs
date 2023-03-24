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

			var sql = $"SELECT * FROM {nombreTabla} C " +
					  $"INNER JOIN Producto P ON P.cliente_id = C.cliente_id " +
					  $"INNER JOIN Transaccion T ON P.producto_id = T.producto_id";
			var cliente = await connection.QueryAsync<ClienteConProducto, ProductoConTransaccion, Transaccion, ClienteConProducto>(sql,
			(cliente, producto, transaccion) => {
				if (cliente.Productos == null)
				{
					if (producto.Transacciones == null)
					{
						producto.Transacciones = new List<Transaccion>();
					}
					cliente.Productos = new List<ProductoConTransaccion>();
				}
				producto.Transacciones.Add(transaccion);
				cliente.Productos.Add(producto);
				return cliente;
			},
			splitOn: "producto_id");

			connection.Close();
			return (List<ClienteConProducto>)cliente;
		}

		public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			var sql = $"SELECT * FROM {nombreTabla} C " +
					  $"INNER JOIN Tarjeta A ON C.cliente_id = A.cliente_id " +
					  $"INNER JOIN Transaccion T ON A.tarjeta_id = T.tarjeta_id";
			var cliente = await connection.QueryAsync<ClienteConTarjeta, TarjetaConTransaccion, Transaccion, ClienteConTarjeta>(sql,
			(cliente, tarjeta, transaccion) => {
				if (cliente.Tarjetas == null)
				{
					if (tarjeta.Transacciones == null)
					{
						tarjeta.Transacciones = new List<Transaccion>();
					}
					cliente.Tarjetas = new List<TarjetaConTransaccion>();
				}
				tarjeta.Transacciones.Add(transaccion);
				cliente.Tarjetas.Add(tarjeta);
				return cliente;
			},
			splitOn: "tarjeta_id");

			connection.Close();
			return (List<ClienteConTarjeta>)cliente;
		}

		public async Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			var sql = $"SELECT * FROM {nombreTabla} C " +
					  $"INNER JOIN Cuenta A ON C.cliente_id = A.cliente_id " +
					  $"INNER JOIN Transaccion T ON A.cuenta_id = T.cuenta_id";
			var cliente = await connection.QueryAsync<ClienteConCuenta, CuentaConTransaccion, Transaccion, ClienteConCuenta>(sql,
			(cliente, cuenta, transaccion) => {
				if (cliente.Cuentas == null)
				{
					if (cuenta.Transacciones == null)
					{
						cuenta.Transacciones = new List<Transaccion>();
					}
					cliente.Cuentas = new List<CuentaConTransaccion>();
				}
				cuenta.Transacciones.Add(transaccion);
				cliente.Cuentas.Add(cuenta);
				return cliente;
			},
			splitOn: "cuenta_id");

			connection.Close();
			return (List<ClienteConCuenta>)cliente;
		}
	}
}
