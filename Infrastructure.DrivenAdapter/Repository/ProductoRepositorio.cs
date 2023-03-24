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

    public class ProductoRepositorio : IProductoRepositorio
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "Producto";

        public ProductoRepositorio(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<InsertarNuevoProducto> InsertarProductoAsync(InsertarNuevoProducto producto)
        {
            Guard.Against.Null(producto, nameof(producto));
            Guard.Against.NullOrEmpty(producto.Cliente_Id.ToString(), nameof(producto.Cliente_Id));
            Guard.Against.NullOrEmpty(producto.Tipo_Producto, nameof(producto.Tipo_Producto));
            Guard.Against.NullOrEmpty(producto.Descripcion, nameof(producto.Descripcion));
            Guard.Against.NullOrEmpty(producto.Plazo.ToString(), nameof(producto.Plazo));
            Guard.Against.NullOrEmpty(producto.Monto.ToString(), nameof(producto.Monto));
            Guard.Against.NullOrEmpty(producto.Tasa_Interes.ToString(), nameof(producto.Tasa_Interes));
            Guard.Against.NullOrEmpty(producto.Estado, nameof(producto.Estado));


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarProducto = new
            {
                Cliente_Id = producto.Cliente_Id,
                Tipo_Producto = producto.Tipo_Producto,
                Descripcion = producto.Descripcion,
                Plazo = producto.Plazo,
                Monto = producto.Monto,
                Tasa_Interes = producto.Tasa_Interes,
                Estado = producto.Estado
            };

            string insertProductoQuery = $"INSERT INTO {nombreTabla}  (Cliente_Id, Tipo_Producto, Descripcion, Plazo, Monto, Tasa_Interes, Estado) VALUES (@Cliente_Id, @Tipo_Producto, @Descripcion, @Plazo, @Monto, @Tasa_Interes, @Estado); SELECT SCOPE_IDENTITY();";
            int productoId = await connection.ExecuteScalarAsync<int>(insertProductoQuery, insertarProducto);

            //Crear objeto con la informacion de la nueva transaccion
            var nuevaTransaccion = new
            {
                cuenta_id = (int?)null,
                tarjeta_id = (int?)null,
                producto_id = productoId,
                fecha = DateTime.Today,
                tipo_transaccion = "Creacion de producto",
                descripcion = "Se crea el producto para el usuario con el cliente_id " + producto.Cliente_Id + ".   ",
                monto = producto.Monto
            };
            // Insertar la nueva transacción.
            string insertTransaccionQuery = $"INSERT INTO Transaccion (cuenta_id, tarjeta_id, producto_id, fecha, tipo_transaccion, descripcion, monto) VALUES (@cuenta_id, @tarjeta_id, @producto_id, @fecha, @tipo_transaccion, @descripcion, @monto)";
            int resultadoTransaccion = await connection.ExecuteAsync(insertTransaccionQuery, nuevaTransaccion);

            connection.Close();

            return producto;

        }

        public async Task<List<Producto>> TraerTodosLosProductos()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Producto>(query);

            connection.Close();
            return resultado.ToList();
        }
    }
}
