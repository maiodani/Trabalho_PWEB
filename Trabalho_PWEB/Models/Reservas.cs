using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Reservas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "ID Reservante")]
        public string idReservante { get; set; }
        [Required]
        [Display(Name = "ID Veiculo")]
        public int IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; }
        [Required]
        [Display(Name = "Data Levantamento")]
        public DateTime DataLevantamento { get; set; }
        [Display(Name = "Data Entrega")]
        [Required]
        public DateTime DataEntrega { get; set; }
        public int? idReservaEstadoCarro { get; set; }
        [Required]
        [Display(Name = "Reserva Aceite pela Empresa?")]
        public bool ReservaAceite { get; set; } //SE FOI ACEITE OU NAO PELA EMPRESA
        [Display(Name = "Reserva a Decorrer?")]
        [Required]
        public bool Ativa { get; set; }//SE AINDA ESTA A DECORRER OU NAO
        [Required]
        public bool Acabou{ get; set; }
    }
}
