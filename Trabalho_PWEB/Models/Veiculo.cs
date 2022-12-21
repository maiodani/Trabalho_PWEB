using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Veiculo
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Matricula")]
        public string Matricula { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Localização { get; set; }
        [Required]
        public float Preco { get; set; }

        public int? EmpresaId { get; set; }

        //PROXIMOS DOIS NAO DEVEM APARECER NO REGISTER DE UM VEICULO POR UMA EMPRESA
        public bool Ócupado { get; set; } //SE ESTIVER A SER OCUPADO TEM A DATA DE ENTREGA - USADO PARA PESQUIAS POR DATA DE ENTREGA
        public DateTime DataEntrega { get; set; }
    }
}
