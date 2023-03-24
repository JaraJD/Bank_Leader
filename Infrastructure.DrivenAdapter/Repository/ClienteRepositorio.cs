using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public Task<Cliente> ObtenerClientePorIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<ClienteConProducto>> ObtenerClienteProductoAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Cliente>> TraerTodosLosClientes()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Cliente>(query);
            Guard.Against.Null(resultado, nameof(resultado));

            connection.Close();
            return resultado.ToList();
        }

		public Task<List<Cliente>> TraerTodosLosClientesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
