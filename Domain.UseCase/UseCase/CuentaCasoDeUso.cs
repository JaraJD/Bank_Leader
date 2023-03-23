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
    public class CuentaCasoDeUso : ICuentaCasoDeUso
    {
        private readonly ICuentaRepositorio cuentaRepositorio;

        public CuentaCasoDeUso(ICuentaRepositorio cuentaRepositorio)
        {
            this.cuentaRepositorio = cuentaRepositorio;
        }

        public async Task<Cuenta> AgregarCuenta(Cuenta cuenta)
        {
            return await cuentaRepositorio.InsertarCuentaAsync(cuenta);
        }

        public async Task<List<Cuenta>> ObtenerCuentas()
        {
            return await cuentaRepositorio.TraerTodasLasCuentas();
        }
    }
}
