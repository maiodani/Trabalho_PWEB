using Microsoft.AspNetCore.Identity;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Data
{
    public enum Roles
    {
        Admin,
        Cliente,
        Funcionario,
        Gestor
    }
    public static class Inicializacao
    {
        public static async Task CriaDadosIniciais(UserManager<ApplicationUser>
userManager, RoleManager<IdentityRole> roleManager)
        {
            //Adicionar default Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Funcionario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Gestor.ToString()));
            //Adicionar Default User - Admin

            var user = await userManager.FindByEmailAsync("TiagoF2001@gmail.com");
            await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
        }
    }
}
