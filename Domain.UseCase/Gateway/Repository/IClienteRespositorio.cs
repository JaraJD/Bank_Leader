using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway.Repository
{
    public interface IClienteRespositorio
    {
        Task<Cliente> InsertarClienteAsync(Cliente cliente);
        Task<List<Cliente>> TraerTodosLosClientes();
    }
}
