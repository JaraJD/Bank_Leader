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
	public class ProductoControlador
	{
		private readonly IProductoCasoDeUso _productoCasoDeUso;
		private readonly IMapper _mapper;

		public ProductoControlador(IProductoCasoDeUso productoCasoDeUso, IMapper mapper)
		{
			_productoCasoDeUso = productoCasoDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Producto>> Obtener_Listado_Directores()
		{
			return await _productoCasoDeUso.ObtenerProductos();
		}

		[HttpPost]
		public async Task<InsertarNuevoProducto> Registrar_Director(InsertarNuevoProducto command)
		{
			return await _productoCasoDeUso.AgregarProducto(command);
		}
	}
}
