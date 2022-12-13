using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nome da Categoria")]
        public string NomeCategoria { get; set; }
    }
}
