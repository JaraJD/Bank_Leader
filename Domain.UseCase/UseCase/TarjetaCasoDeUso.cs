using Domain.Entities.Commands;
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
    public class TarjetaCasoDeUso : ITarjetaCasoDeUso
    {
        private readonly ITarjetaRepositorio tarjetaRepositorio;

        public TarjetaCasoDeUso(ITarjetaRepositorio tarjetaRepositorio)
        {
            this.tarjetaRepositorio = tarjetaRepositorio;
        }

        public async Task<InsertarNuevaTarjeta> AgregarTarjeta(InsertarNuevaTarjeta tarjeta)
        {
            return await tarjetaRepositorio.InsertarTarjetaAsync(tarjeta);
        }

        public async Task<List<Tarjeta>> ObtenerTarjetas()
        {
            return await tarjetaRepositorio.TraerTodasLasTarjetas();
        }
    }
}
