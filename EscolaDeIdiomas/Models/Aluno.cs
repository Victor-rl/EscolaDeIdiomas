using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Aluno
    {
        public int Id {  get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(13)]
        public string CPF { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; }
    }
}
