using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Empresa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nome da Empresa")]
        public string Nome { get; set; }
        public int Avaliação { get; set; }
        public ICollection<Veiculo> Veiculo { get; set; }
        public ICollection<ApplicationUser> ListaUtilizadores { get; set; }
    }
}
