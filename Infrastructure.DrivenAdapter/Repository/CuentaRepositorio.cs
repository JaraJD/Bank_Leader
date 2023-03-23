using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repository
{
	public class CuentaRepositorio : ICuentaRepositorio
	{
		private readonly IDbConnectionBuilder _dbConnectionBuilder;

		public CuentaRepositorio(IDbConnectionBuilder dbConnectionBuilder)
		{
			_dbConnectionBuilder = dbConnectionBuilder;
		}

		public Task<InsertarNuevaCuenta> InsertarCuentaAsync(InsertarNuevaCuenta cuenta)
		{
			throw new NotImplementedException();
		}

		public Task<List<Cuenta>> TraerTodasLasCuentas()
		{
			throw new NotImplementedException();
		}
	}
}
