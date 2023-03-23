using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Bank.AppService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductoControlador
	{
		//private readonly IProductoCasoDeUso _productoCasoDeUso;
		//private readonly IMapper _mapper;

		//public ProductoControlador(IProductoCasoDeUso productoCasoDeUso, IMapper mapper)
		//{
		//	_productoCasoDeUso = productoCasoDeUso;
		//	_mapper = mapper;
		//}

		//[HttpGet]
		//public async Task<List<Producto>> Obtener_Listado_Directores()
		//{
		//	return await _directorUseCase.ObtenerListaDirectores();
		//}

		//[HttpPost]
		//public async Task<Producto> Registrar_Director([FromBody] InsertarNuevoProducto command)
		//{
		//	return await _directorUseCase.AgregarDirector(_mapper.Map<Producto>(command));
		//}
	}
}
