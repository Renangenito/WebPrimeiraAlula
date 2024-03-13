using System.ComponentModel.DataAnnotations;

namespace WebPrimeiraAlula.Models
{
    public class FornecedorModel : EnderecoModel
    {
        [Required(ErrorMessage = "O CNPJ é obrigatório!")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Este campo deve ter 18 caracteres!")]
        public string CNPJ { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O nome completo é obrigatório!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Este campo deve ter no mínimo 5 e no máximo 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório!")]
        [StringLength(15, MinimumLength = 0, ErrorMessage = "Este campo não pode ficar em branco!")]
        public string? Telefone { get; set; }

        [Display(Name = "Data Inclusão")]
        [Required(ErrorMessage = "Data Inclusão é obrigatória!")]
        public DateTime DataInclusao { get; set; }

        [Display(Name = "Data Alteração")]
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
