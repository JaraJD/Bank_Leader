using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repository
{
	public class TransaccionesRepositorio : ITransaccionesRepositorio
	{
		public Task<List<Transaccion>> TraerTodasLasTransacciones()
		{
			throw new NotImplementedException();
		}
	}
}
