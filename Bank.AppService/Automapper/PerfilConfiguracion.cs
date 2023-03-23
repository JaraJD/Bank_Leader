using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System.IO;

namespace Bank.AppService.Automapper
{
	public class PerfilConfiguracion : Profile
	{
		public PerfilConfiguracion()
		{
			CreateMap<InsertarNuevaCuenta, Cuenta>().ReverseMap();
			CreateMap<InsertarNuevaTarjeta, Tarjeta>().ReverseMap();
			CreateMap<InsertarNuevaTransaccion, Transaccion>().ReverseMap();
			CreateMap<InsertarNuevoCliente, Cliente>().ReverseMap();
			CreateMap<InsertarNuevoProducto, Producto>().ReverseMap();
		}
	}
}
