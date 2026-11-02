using RestauranteApi.Middlewares;
using RestauranteApi.Repositorios;
using RestauranteApi.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Activa los controladores (Clientes, Mesas y Reservas).
builder.Services.AddControllers();

// Inyección de dependencias:
// AddSingleton = una sola instancia mientras la API esté encendida,
// así las listas en memoria no se reinician en cada petición.
builder.Services.AddSingleton<IRepositorioClientes, RepositorioClientesMemoria>();
builder.Services.AddSingleton<IRepositorioMesas, RepositorioMesasMemoria>();
builder.Services.AddSingleton<IRepositorioReservas, RepositorioReservasMemoria>();

builder.Services.AddSingleton<IServicioClientes, ServicioClientes>();
builder.Services.AddSingleton<IServicioMesas, ServicioMesas>();
builder.Services.AddSingleton<IServicioReservas, ServicioReservas>();

var app = builder.Build();

// El middleware de errores va primero para atrapar cualquier excepción.
app.UseMiddleware<ManejoErroresMiddleware>();

app.MapControllers();

app.Run();
