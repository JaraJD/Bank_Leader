using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway
{
    internal interface IProductoCasoDeUso
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> AgregarProducto(Producto producto);
    }
}
