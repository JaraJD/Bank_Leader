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
        private readonly IClienteRespositorio clienteRespositorio;

        public ClienteCasoDeUso(IClienteRespositorio clienteRespositorio)
        {
            this.clienteRespositorio = clienteRespositorio;
        }

        public async Task<Cliente> AgregarCliente(Cliente cliente)
        {
            return await clienteRespositorio.InsertarClienteAsync(cliente);
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await clienteRespositorio.TraerTodosLosClientes();
        }
    }
}
