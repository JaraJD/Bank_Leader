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
            Guard.Against.NullOrEmpty(producto.Id_Cliente.ToString(), nameof(producto.Id_Cliente));
            Guard.Against.NullOrEmpty(producto.Tipo_Producto, nameof(producto.Tipo_Producto));
            Guard.Against.NullOrEmpty(producto.Descripcion, nameof(producto.Descripcion));
            Guard.Against.NullOrEmpty(producto.Plazo.ToString(), nameof(producto.Plazo));
            Guard.Against.NullOrEmpty(producto.Monto.ToString(), nameof(producto.Monto));
            Guard.Against.NullOrEmpty(producto.Tasa_Interes.ToString(), nameof(producto.Tasa_Interes));
            Guard.Against.NullOrEmpty(producto.Estado, nameof(producto.Estado));


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var insertarProducto = new
            {
                Id_Cliente = producto.Id_Cliente,
                Tipo_Producto = producto.Tipo_Producto,
                Descripcion = producto.Descripcion,
                Plazo = producto.Plazo,
                Monto = producto.Monto,
                Tasa_Interes = producto.Tasa_Interes,
                Estado = producto.Estado
            };

            string query = $"INSERT INTO {nombreTabla}  (Id_Cliente, Tipo_Producto, Descripcion, Plazo, Monto, Tasa_Interes, Estado) VALUES (@Id_Cliente, @Tipo_Producto, @Descripcion, @Plazo, @Monto, @Tasa_Interes, @Estado)";
            var resultado = await connection.ExecuteAsync(query, insertarProducto);

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
