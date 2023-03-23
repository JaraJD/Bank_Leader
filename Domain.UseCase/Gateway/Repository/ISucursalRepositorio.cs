using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway.Repository
{
    public interface ISucursalRepositorio
    {
        Task<Sucursal> InsertarSucursalAsync(Sucursal sucursal);
        Task<List<Sucursal>> TraerTodoasLasSucursales();
    }
}
