using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using Infrastructure.DrivenAdapter.Gateway;

namespace Infrastructure.DrivenAdapter.Repository
{
    public class TarjetaRepositorio : ITarjetaRepositorio
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "Tarjeta";
        public TarjetaRepositorio(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<InsertarNuevaTarjeta> InsertarTarjetaAsync(InsertarNuevaTarjeta tarjeta)
        {
            Guard.Against.Null(tarjeta, nameof(tarjeta));
            Guard.Against.NullOrEmpty(tarjeta.Cliente_Id.ToString(), nameof(tarjeta.Cliente_Id));
            Guard.Against.NullOrEmpty(tarjeta.Tipo_Tarjeta, nameof(tarjeta.Tipo_Tarjeta));
            Guard.Against.NullOrEmpty(tarjeta.Fecha_Emision.ToString(), nameof(tarjeta.Fecha_Emision));
            Guard.Against.NullOrEmpty(tarjeta.Fecha_Vencimiento.ToString(), nameof(tarjeta.Fecha_Vencimiento));
            Guard.Against.NullOrEmpty(tarjeta.Limite_Credito.ToString(), nameof(tarjeta.Limite_Credito));
            Guard.Against.NullOrEmpty(tarjeta.Estado, nameof(tarjeta.Estado));


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarNuevaTarjeta = new
            {
                Cliente_Id = tarjeta.Cliente_Id,
                Tipo_Tarjeta = tarjeta.Tipo_Tarjeta,
                Fecha_Emision = tarjeta.Fecha_Emision,
                Fecha_Vencimiento = tarjeta.Fecha_Vencimiento,
                Limite_Credito = tarjeta.Limite_Credito,
                Estado = tarjeta.Estado
            };

            string query = $"INSERT INTO {nombreTabla}  (Cliente_Id, Tipo_Tarjeta, Fecha_Emision, Fecha_Vencimiento, Limite_Credito, Estado) VALUES (@Cliente_Id, @Tipo_Tarjeta, @Fecha_Emision, @Fecha_Vencimiento, @Limite_Credito, @Estado); SELECT SCOPE_IDENTITY();";
            int tarjetaId = await connection.ExecuteScalarAsync<int>(query, insertarNuevaTarjeta);

            var nuevaTransaccion = new
            {
                cuenta_id = (int?)null,
                tarjeta_id = tarjetaId,
                producto_id = (int?)null,
                fecha = DateTime.Today,
                tipo_transaccion = "Creacion de tarjeta",
                descripcion = "Se crea la tarjeta para el usuario con el cliente_id " + tarjeta.Cliente_Id + ".   ",
                monto = 0
            };

            // Insertar la nueva transacción.
            string insertTransaccionQuery = $"INSERT INTO Transaccion (cuenta_id, tarjeta_id, producto_id, fecha, tipo_transaccion, descripcion, monto) VALUES (@cuenta_id, @tarjeta_id, @producto_id, @fecha, @tipo_transaccion, @descripcion, @monto)";
            int resultadoTransaccion = await connection.ExecuteAsync(insertTransaccionQuery, nuevaTransaccion);

            connection.Close();

            return tarjeta;
        }

        public async Task<List<Tarjeta>> TraerTodasLasTarjetas()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Tarjeta>(query);

            return resultado.ToList();
        }
    }
}
