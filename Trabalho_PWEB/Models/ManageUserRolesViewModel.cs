using Microsoft.AspNetCore.Mvc;

namespace Trabalho_PWEB.Models
{
    public class ManageUserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
