using Microsoft.AspNetCore.Identity;

namespace Trabalho_PWEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? PrimeiroNome { get; set; }
        [PersonalData]
        public string? UltimoNome { get; set; }
        public int? EmpresaId { get; set; }
    }
}
