using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class CreateVeiculoModelView
    {
        [Required]
        [Display(Name = "Matricula")]
        public string Matricula { get; set; }
        [Required]
        [Display(Name = "Marca")]
        public string Marca { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }
        [Required]
        [Display(Name = "Localização")]
        public string Localizacao { get; set; }
        [Display(Name = "Categoria")]
        [Required]
        public List<string> Categorias { get; set; }
        public List<int>? CategoriasID { get; set; }
        [Required]
        [Display(Name = "Estado do Carro")]
        public string Estado { get; set; }
        [Display(Name = "Preço por dia")]
        [Required]
        public float Preco { get; set; }
        [Display(Name = "Ativo")]
        [Required]
        public bool Ativo { get; set; }
    }
}
