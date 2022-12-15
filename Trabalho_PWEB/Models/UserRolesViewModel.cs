using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class UserRolesViewModel
    {
        [Display(Name = "ID")]
        public string UserId { get; set; }
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }
    }
}