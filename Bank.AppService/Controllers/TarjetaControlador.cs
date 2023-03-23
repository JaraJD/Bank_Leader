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
		private readonly ITarjetaCasoDeUso _tarjetaCaspDeUso;
		private readonly IMapper _mapper;

		public TarjetaControlador(ITarjetaCasoDeUso tarjetaCaspDeUso, IMapper mapper)
		{
			_tarjetaCaspDeUso = tarjetaCaspDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Tarjeta>> Obtener_Listado_Directores()
		{
			return await _tarjetaCaspDeUso.ObtenerTarjetas();
		}

		[HttpPost]
		public async Task<Tarjeta> Registrar_Director([FromBody] InsertarNuevaTarjeta command)
		{
			return await _tarjetaCaspDeUso.AgregarTarjeta(_mapper.Map<Tarjeta>(command));
		}
	}
}
