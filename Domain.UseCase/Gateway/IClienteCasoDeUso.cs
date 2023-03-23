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
        Task<Cliente> AgregarCliente(Cliente cliente);
    }
}
