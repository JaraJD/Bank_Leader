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
	public class TarjetaControlador
	{
		private readonly ITarjetaCasoDeUso _tarjetaCasoDeUso;
		private readonly IMapper _mapper;

		public TarjetaControlador(ITarjetaCasoDeUso tarjetaCaspDeUso, IMapper mapper)
		{
			_tarjetaCasoDeUso = tarjetaCaspDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Tarjeta>> Obtener_Listado_Directores()
		{
			return await _tarjetaCasoDeUso.ObtenerTarjetas();
		}

		[HttpPost]
		public async Task<InsertarNuevaTarjeta> Registrar_Director(InsertarNuevaTarjeta command)
		{
			return await _tarjetaCasoDeUso.AgregarTarjeta(command);
		}
	}
}
