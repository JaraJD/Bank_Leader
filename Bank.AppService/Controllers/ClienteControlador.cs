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


        [HttpPost]
        public async Task<InsertarNuevoCliente> Registrar_Director(InsertarNuevoCliente command)
        {
            return await _clienteCasoDeUso.AgregarCliente(command);
        }

        [HttpGet]
        public async Task<List<Cliente>> Obtener_Listado_Clientes()
        {
            return await _clienteCasoDeUso.ObtenerClientes();
        }

        [HttpGet("/TransaccionesCuenta")]
        public async Task<List<ClienteConCuenta>> Transacciones_De_La_Cuenta()
        {
            return await _clienteCasoDeUso.ObtenerClienteTransacciones();
        }

        [HttpGet("/TransaccionesTarjeta")]
        public async Task<List<ClienteConTarjeta>> Transacciones_De_La_Tarjeta()
        {
            return await _clienteCasoDeUso.ObtenerClienteTarjeta();
        }

        [HttpGet("/TransaccionesProducto")]
        public async Task<List<ClienteConProducto>> Transacciones_Del_Producto()
        {
            return await _clienteCasoDeUso.ObtenerClienteProducto();
        }

        [HttpGet("{id:int}")]
        public async Task<Cliente> Obtener_Cliente_Por_Id(int id)
        {
            return await _clienteCasoDeUso.ObtenerClientePorId(id);
        }

        

    }
}
