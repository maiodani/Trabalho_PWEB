using System.ComponentModel.DataAnnotations;

namespace Trabalho_PWEB.Models
{
    public class ListVeiculosViewModel
    {
        [Required]
        public List<Veiculo> veiculos { get; set; }
        [Required]
        public List<Empresa> empresa { get; set; }
        [Required]
        public string Localizacao { get; set; }
        [Required]
        public string DataLevantamento { get; set; }
        [Required]
        public string DataEntrega { get; set; }
    }
}
