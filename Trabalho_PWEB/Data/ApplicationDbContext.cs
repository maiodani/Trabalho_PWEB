using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Trabalho_PWEB.Models.Categoria> Categoria { get; set; }
        public DbSet<Trabalho_PWEB.Models.Empresa> Empresa { get; set; }
        public DbSet<Trabalho_PWEB.Models.Veiculo> Veiculo { get; set; }
        public DbSet<Trabalho_PWEB.Models.Reservas> Reservas { get; set; }
        public DbSet<Trabalho_PWEB.Models.EstadoCarro> EstadoCarro { get; set; }

    }
}