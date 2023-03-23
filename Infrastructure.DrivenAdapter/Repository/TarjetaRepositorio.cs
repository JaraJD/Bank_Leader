using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repository
{
	public class TarjetaRepositorio : ITarjetaRepositorio
	{
		public Task<InsertarNuevaTarjeta> InsertarTarjetaAsync(InsertarNuevaTarjeta tarjeta)
		{
			throw new NotImplementedException();
		}

		public Task<List<Tarjeta>> TraerTodasLasTarjetas()
		{
			throw new NotImplementedException();
		}
	}
}
