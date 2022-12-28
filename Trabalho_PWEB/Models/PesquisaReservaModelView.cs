using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class PesquisaReservaModelView
    {
        [Required]
        public List<Categoria> Categorias { get; set; }
        [Required]
        public string Localizacao { get; set; }
        [Required]
        public DateOnly DataLevantamento { get; set; }
        [Required]
        public DateOnly DataEntrega { get; set; }
    }
}
