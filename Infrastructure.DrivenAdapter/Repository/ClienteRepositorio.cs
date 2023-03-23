using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
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


        public async Task<Cliente> InsertarClienteAsync(Cliente cliente)
        {
            Guard.Against.Null(cliente, nameof(cliente));
            Guard.Against.NullOrEmpty(cliente.Nombre, nameof(cliente.Nombre));
            Guard.Against.NullOrEmpty(cliente.Apellidos, nameof(cliente.Apellidos));
            Guard.Against.NullOrEmpty(cliente.Fecha_Nacimiento.ToString(), nameof(cliente.Fecha_Nacimiento));
            Guard.Against.NullOrEmpty(cliente.Direccion, nameof(cliente.Direccion));
            Guard.Against.NullOrEmpty(cliente.Telefono, nameof(cliente.Telefono));
            Guard.Against.NullOrEmpty(cliente.Correo, nameof(cliente.Correo));
            Guard.Against.NullOrEmpty(cliente.Ocupacion, nameof(cliente.Ocupacion));
            Guard.Against.NullOrEmpty(cliente.Genero, nameof(cliente.Genero));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarNuevoCliente = new
            {
                nombre = cliente.Nombre,
                apellidos = cliente.Apellidos,
                fecha_nacimiento = cliente.Fecha_Nacimiento,
                direccion = cliente.Direccion,
                telefono = cliente.Telefono,
                correo = cliente.Correo,
                ocupacion = cliente.Ocupacion,
                genero = cliente.Genero
            };
            string query = $"INSERT INTO {nombreTabla} (nombre, apellidos, fecha_nacimiento, direccion, telefono, correo, ocupacion, genero) VALUES (@nombre, @apellidos, @fecha_nacimiento, @direccion, @telefono, @correo, @ocupacion, @genero)";
            var resultado = await connection.ExecuteAsync(query, insertarNuevoCliente);
            return cliente;
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
    }
}
