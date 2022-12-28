using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Reservas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string idReservante { get; set; }
        [Required]
        public int IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; }
        [Required]
        public DateTime DataLevantamento { get; set; }
        [Required]
        public DateTime DataEntrega { get; set; }
        public int? idReservaEstadoCarro { get; set; }
        [Required]
        public bool Ativa { get; set; }//SE AINDA ESTA A DECORRER OU NAO
    }
}
