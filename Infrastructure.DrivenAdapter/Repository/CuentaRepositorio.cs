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
            Guard.Against.NullOrEmpty(cuenta.Cliente_Id.ToString(), nameof(cuenta.Cliente_Id));
            Guard.Against.NullOrEmpty(cuenta.Tipo_Cuenta, nameof(cuenta.Tipo_Cuenta));
            Guard.Against.NullOrEmpty(cuenta.Saldo.ToString(), nameof(cuenta.Saldo));
            Guard.Against.NullOrEmpty(cuenta.Fecha_Apertura.ToString(), nameof(cuenta.Fecha_Apertura));
            Guard.Against.NullOrEmpty(cuenta.Fecha_Cierre.ToString(), nameof(cuenta.Fecha_Cierre));
            Guard.Against.NullOrEmpty(cuenta.Tasa_Interes.ToString(), nameof(cuenta.Tasa_Interes));
            Guard.Against.NullOrEmpty(cuenta.Estado, nameof(cuenta.Estado));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarNuevaCuenta = new
            {
                Cliente_Id = cuenta.Cliente_Id,
                Tipo_Cuenta = cuenta.Tipo_Cuenta,
                Saldo = cuenta.Saldo,
                Fecha_Apertura = cuenta.Fecha_Apertura,
                Fecha_Cierre = cuenta.Fecha_Cierre,
                Tasa_Interes = cuenta.Tasa_Interes,
                Estado = cuenta.Estado
            };

            // Insertar la nueva cuenta y obtener su cuenta_id.
            string insertCuentaQuery = $"INSERT INTO {nombreTabla} (Cliente_Id, Tipo_Cuenta, Saldo, Fecha_Apertura, Fecha_Cierre, Tasa_Interes, Estado) VALUES (@Cliente_Id, @Tipo_Cuenta, @Saldo, @Fecha_Apertura, @Fecha_Cierre, @Tasa_Interes, @Estado); SELECT SCOPE_IDENTITY();";
            int cuentaId = await connection.ExecuteScalarAsync<int>(insertCuentaQuery, insertarNuevaCuenta);

            // Crear un objeto con la información de la nueva transacción.
            var nuevaTransaccion = new
            {
                cuenta_id = cuentaId,
                tarjeta_id = (int?)null,
                producto_id = (int?)null,
                fecha = DateTime.Today,
                tipo_transaccion = "Creacion de cuenta",
                descripcion = "Se crea la cuenta del usuario con el cliente_id " + cuenta.Cliente_Id + ".   ",  // El espacio al final es para que el campo tenga el mismo tamaño que los otros campos de la tabla.
                monto = cuenta.Saldo
            };

            // Insertar la nueva transacción.
            string insertTransaccionQuery = $"INSERT INTO Transaccion (cuenta_id, tarjeta_id, producto_id, fecha, tipo_transaccion, descripcion, monto) VALUES (@cuenta_id, @tarjeta_id, @producto_id, @fecha, @tipo_transaccion, @descripcion, @monto)";
            int resultadoTransaccion = await connection.ExecuteAsync(insertTransaccionQuery, nuevaTransaccion);

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
