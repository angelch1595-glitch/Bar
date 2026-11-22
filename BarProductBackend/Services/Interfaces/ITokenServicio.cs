using BarProductBackend.Models;

namespace BarProductBackend.Services.Interfaces;

public interface ITokenServicio
{
    // Generar, recibe un usuario y sus roles, y devuelve dos valores llamados Token y Expira.
    (string Token, DateTime Expira) Generar(ApplicationUser usuario,IList<string> roles);
}