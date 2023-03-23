using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway
{
    public interface IClienteCasoDeUso
    {
        Task<List<Cliente>> ObtenerClientes();
        Task<InsertarNuevoCliente> AgregarCliente(InsertarNuevoCliente cliente);
        Task<Cliente> ObtenerClientePorId(int id);
        Task <List<ClienteConCuenta>> ObtenerClienteTransacciones();
        Task <List<ClienteConTarjeta>> ObtenerClienteTarjeta();
        Task <List<ClienteConProducto>> ObtenerClienteProducto();
	}
}
