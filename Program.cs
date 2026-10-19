using RestauranteApi.Middlewares;
using RestauranteApi.Repositories;
using RestauranteApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IMesaRepository, MesaRepositoryMemoria>();
builder.Services.AddSingleton<IReservaRepository, ReservaRepositoryMemoria>();
builder.Services.AddSingleton<IClienteRepository, ClienteRepositoryMemoria>();

builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMesaService, MesaService>();

var app = builder.Build();

app.UseMiddleware<ManejoErroresMiddleware>();

app.MapGet("/", () => Results.Ok(new
{
    aplicacion = "RestauranteApi",
    descripcion = "API REST para administrar clientes, mesas y reservas.",
    endpoints = new[] { "/api/clientes", "/api/mesas", "/api/reservas" },
    nota = "Esta aplicacion no tiene interfaz grafica; consulta las rutas REST o utiliza Postman."
}));

app.MapControllers();

app.Run();
