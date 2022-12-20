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
        [Display(Name = "Estado da Subscrição")]
        public bool EstadoSubscricao { get; set; }
        [Display(Name = "Avaliação")]
        public int Avaliação { get; set; }
        [Display(Name = "Lista de Veiculos")]
        public ICollection<Veiculo> Veiculos { get; set; }
        public ICollection<ApplicationUser> ListaFuncionarios { get; set; }
    }
}
