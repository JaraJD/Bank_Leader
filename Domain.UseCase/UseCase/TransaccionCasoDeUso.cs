using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.UseCase
{
    public class TransaccionCasoDeUso : ITransaccionCasoDeUso
    {

        private readonly ITransaccionesRepositorio transaccionesRepositorio;

        public TransaccionCasoDeUso(ITransaccionesRepositorio transaccionesRepositorio)
        {
            this.transaccionesRepositorio = transaccionesRepositorio;
        }

        public async Task<List<Transaccion>> ObtenerTransacciones()
        {
            return await transaccionesRepositorio.TraerTodasLasTransacciones();
        }
    }
}
