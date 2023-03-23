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
    public class EmpleadoCasoDeUso : IEmpleadoCasoDeUso
    {
        private readonly IEmpleadoRepositorio empleadoRepositorio;

        public EmpleadoCasoDeUso(IEmpleadoRepositorio empleadoRepositorio)
        {
            this.empleadoRepositorio = empleadoRepositorio;
        }

        public async Task<Empleado> AgregarEmpleado(Empleado empleado)
        {
            return await empleadoRepositorio.InsertarEmpleadoAsync(empleado);
        }

        public async Task<List<Empleado>> ObtenerEmpleados()
        {
            return await empleadoRepositorio.TraerTodosLosEmpleados();
        }
    }
}
