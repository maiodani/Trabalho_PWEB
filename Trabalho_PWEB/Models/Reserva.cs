using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ApplicationUser Reservante { get; set; }
        [Required]
        public Veiculo Veiculo { get; set; }
        [Required]
        public DateOnly DataLevantamento { get; set; }
        [Required]
        public DateOnly DataEntrega { get; set; }
        public EstadoVeiculo EstadoVeiculoEntrega { get; set; }
        public EstadoVeiculo EstadoVeiculoReceba { get; set; }

    }
}
