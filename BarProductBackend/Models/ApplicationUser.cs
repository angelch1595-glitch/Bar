using Microsoft.AspNetCore.Identity;
namespace BarProductBackend.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
