using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Bank.AppService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuentaControlador
	{
		private readonly ICuentaCasoDeUso _cuentaCasoDeUso;
		private readonly IMapper _mapper;

		public CuentaControlador(ICuentaCasoDeUso cuentaCasoDeUso, IMapper mapper)
		{
			_cuentaCasoDeUso = cuentaCasoDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Cuenta>> Obtener_Listado_Directores()
		{
			return await _cuentaCasoDeUso.ObtenerCuentas();
		}

		[HttpPost]
		public async Task<InsertarNuevaCuenta> Registrar_Director(InsertarNuevaCuenta command)
		{
			return await _cuentaCasoDeUso.AgregarCuenta(command);
		}
	}
}
