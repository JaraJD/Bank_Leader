﻿using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway.Repository
{
    public interface IClienteRepositorio
    {
        Task<InsertarNuevoCliente> InsertarClienteAsync(InsertarNuevoCliente cliente);
        Task<List<Cliente>> TraerTodosLosClientes();
    }
}
