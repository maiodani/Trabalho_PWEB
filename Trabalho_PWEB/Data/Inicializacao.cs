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
        public static async Task CriaDadosIniciais(UserManager<ApplicationUser>userManager, RoleManager<IdentityRole> roleManager)
        {
            //Adicionar default Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Funcionario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Gestor.ToString()));
            //Adicionar Default User - Admin
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PrimeiroNome = "admin",
                UltimoNome = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser,"Admin1_");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
