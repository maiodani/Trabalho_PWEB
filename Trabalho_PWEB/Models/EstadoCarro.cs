using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class EstadoCarro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int idReserva { get; set; }
        [Required]
        public Reservas reserva { get; set; }
        [Required]
        [Display(Name = "Nº de Quilómetros")]
        public float nKm { get; set; }
        [Required]
        [Display(Name = "Danos no Carro")]
        public bool DanosCarro { get; set; }
        [Required]
        [Display(Name ="Observações")]
        public string Obs { get; set; }
        [Required]
        public string idFuncionario { get; set; }
        [Required]
        public ApplicationUser funcionario { get; set; }
    }
}
