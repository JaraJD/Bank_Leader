using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.UseCase
{
    public class SucursalCasoDeUso : ISucursalCasoDeUso
    {
        private readonly ISucursalRepositorio sucursalRepositorio;

        public SucursalCasoDeUso(ISucursalRepositorio sucursalRepositorio)
        {
            this.sucursalRepositorio = sucursalRepositorio;
        }

        public async Task<Sucursal> AgregarSucursal(Sucursal sucursal)
        {
            return await sucursalRepositorio.InsertarSucursalAsync(sucursal);
        }

        public async Task<List<Sucursal>> ObtenerSucursales()
        {
            return await sucursalRepositorio.TraerTodoasLasSucursales();
        }
    }
}
