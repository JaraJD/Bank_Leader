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
	public class ClienteControlador : ControllerBase
	{
		private readonly IClienteCasoDeUso _clienteCasoDeUso;
		private readonly IMapper _mapper;

		public ClienteControlador(IClienteCasoDeUso clienteCasoDeUso, IMapper mapper)
		{
			_clienteCasoDeUso = clienteCasoDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Cliente>> Obtener_Listado_Directores()
		{
			return await _clienteCasoDeUso.ObtenerClientes();
		}

		[HttpPost]
		public async Task<InsertarNuevoCliente> Registrar_Director(InsertarNuevoCliente command)
		{
			return await _clienteCasoDeUso.AgregarCliente(command);
		}
	}
}
