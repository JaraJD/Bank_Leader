using Ardalis.GuardClauses;
using Dapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repository
{
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "Cuenta";
        public CuentaRepositorio(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<InsertarNuevaCuenta> InsertarCuentaAsync(InsertarNuevaCuenta cuenta)
        {
            Guard.Against.Null(cuenta, nameof(cuenta));
            Guard.Against.NullOrEmpty(cuenta.Id_Cliente.ToString(), nameof(cuenta.Id_Cliente));
            Guard.Against.NullOrEmpty(cuenta.Tipo_Cuenta, nameof(cuenta.Tipo_Cuenta));
            Guard.Against.NullOrEmpty(cuenta.Saldo.ToString(), nameof(cuenta.Saldo));
            Guard.Against.NullOrEmpty(cuenta.Fecha_Apertura.ToString(), nameof(cuenta.Fecha_Apertura));
            Guard.Against.NullOrEmpty(cuenta.Fecha_Cierre.ToString(), nameof(cuenta.Fecha_Cierre));
            Guard.Against.NullOrEmpty(cuenta.Tasa_Interes.ToString(), nameof(cuenta.Tasa_Interes));
            Guard.Against.NullOrEmpty(cuenta.Estado, nameof(cuenta.Estado));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarNuevaCuenta = new
            {
                Id_Cliente = cuenta.Id_Cliente,
                Tipo_Cuenta = cuenta.Tipo_Cuenta,
                Saldo = cuenta.Saldo,
                Fecha_Apertura = cuenta.Fecha_Apertura,
                Fecha_Cierre = cuenta.Fecha_Cierre,
                Tasa_Interes = cuenta.Tasa_Interes,
                Estado = cuenta.Estado
            };

            string query = $"INSERT INTO {nombreTabla} (Id_Cliente, Tipo_Cuenta, Saldo, Fecha_Apertura, Fecha_Cierre, Tasa_Interes, Estado) VALUES (@Id_Cliente, @Tipo_Cuenta, @Saldo, @Fecha_Apertura, @Fecha_Cierre, @Tasa_Interes, @Estado)";
            var resultado = await connection.ExecuteAsync(query, insertarNuevaCuenta);

            connection.Close();
            return cuenta;
        }

        public async Task<List<Cuenta>> TraerTodasLasCuentas()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Cuenta>(query);

            connection.Close();
            return resultado.ToList();
        }
    }
}
