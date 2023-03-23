using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway.Repository
{
    public interface IEmpleadoRepositorio
    {
        Task<Empleado> InsertarEmpleadoAsync(Empleado empleado);
        Task<List<Empleado>> TraerTodosLosEmpleados();
    }
}
