using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class EstadoVeiculo
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int NumeroDeQuilometros { get; set; }
        [Required]
        public bool DadosVeiculo { get; set; }
        public String Observacoes { get; set; }
        [Required]
        public ApplicationUser Funcionario { get; set; } //FUNCIONARIO DE UMA EMPRESA 

    }
}
