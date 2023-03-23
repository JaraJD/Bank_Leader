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
        Task<Cuenta> InsertarCuentaAsync(Cuenta cuenta);
        Task<List<Cuenta>> TraerTodasLasCuentas();
    }
}
