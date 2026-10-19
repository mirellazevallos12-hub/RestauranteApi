using System.Text.RegularExpressions;
using RestauranteApi.Exceptions;

namespace RestauranteApi.Models;

public class Cliente
{
    public string Dni { get; }
    public string NombreCompleto { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;

    public Cliente(string dni, string nombreCompleto, string telefono)
    {
        if (string.IsNullOrWhiteSpace(dni) || !Regex.IsMatch(dni, @"^\d{8}$"))
            throw new ReglaNegocioException("El DNI debe tener exactamente 8 números.");

        Dni = dni;
        Actualizar(nombreCompleto, telefono);
    }

    public void Actualizar(string nombre, string telefono)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono))
            throw new ReglaNegocioException("Nombre y teléfono son obligatorios.");

        NombreCompleto = nombre.Trim();
        Telefono = telefono.Trim();
    }
}
