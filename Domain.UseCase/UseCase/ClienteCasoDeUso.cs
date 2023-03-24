using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Domain.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.UseCase
{
    public class ClienteCasoDeUso : IClienteCasoDeUso
    {
        private readonly IClienteRepositorio clienteRespositorio;

        public ClienteCasoDeUso(IClienteRepositorio clienteRespositorio)
        {
            this.clienteRespositorio = clienteRespositorio;
        }

        public async Task<InsertarNuevoCliente> AgregarCliente(InsertarNuevoCliente cliente)
        {
            return await clienteRespositorio.InsertarClienteAsync(cliente);
        }

		public async Task<ClienteConActivos> ObtenerClienteActivos(int id)
		{
			return await clienteRespositorio.ObtenerClienteActivosAsync(id);
		}

		public async Task<Cliente> ObtenerClientePorId(int id)
		{
			return await clienteRespositorio.ObtenerClientePorIdAsync(id);
		}

		public async Task<List<ClienteConProducto>> ObtenerClienteProducto()
		{
			return await clienteRespositorio.ObtenerClienteProductoAsync();
		}

		public async Task<List<Cliente>> ObtenerClientes()
        {
            return await clienteRespositorio.TraerTodosLosClientesAsync();
        }

		public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjeta()
		{
			return await clienteRespositorio.ObtenerClienteTarjetaAsync();
		}

		public async Task<List<ClienteConCuenta>> ObtenerClienteTransacciones()
		{
			return await clienteRespositorio.ObtenerClienteTransaccionesAsync();
		}
	}
}
