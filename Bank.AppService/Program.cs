using AutoMapper.Data;
using Bank.AppService.Automapper;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Domain.UseCase.Gateway.Repository;
using Domain.UseCase.UseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(PerfilConfiguracion));

builder.Services.AddScoped<IClienteCasoDeUso, ClienteCasoDeUso>();
builder.Services.AddScoped<IClienteRespositorio, ClienteRepositorio>();

builder.Services.AddScoped<ICuentaCasoDeUso, CuentaCasoDeUso>();
builder.Services.AddScoped<ICuentaRepositorio, CuentaRepositorio>();

builder.Services.AddScoped<ITarjetaCasoDeUso, TarjetaCasoDeUso>();
builder.Services.AddScoped<ITarjetaRepositorio, TarjetaRepositorio>();

builder.Services.AddScoped<IProductoCasoDeUso, ProductoCasoDeUso>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();

builder.Services.AddScoped<ITransaccionCasoDeUso, TransaccionCasoDeUso>();
builder.Services.AddScoped<ITransaccionesRepositorio, TransaccionRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
