using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway.Repository
{
    public interface ICuentaRepositorio
    {
        Task<InsertarNuevaCuenta> InsertarCuentaAsync(InsertarNuevaCuenta cuenta);
        Task<List<Cuenta>> TraerTodasLasCuentas();
    }
}
