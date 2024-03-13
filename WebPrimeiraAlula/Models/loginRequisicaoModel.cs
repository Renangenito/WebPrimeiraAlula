using System.ComponentModel.DataAnnotations;

namespace WebPrimeiraAula.Models
{
    public class loginRequisicaoModel
    {

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }

       
    }
}
