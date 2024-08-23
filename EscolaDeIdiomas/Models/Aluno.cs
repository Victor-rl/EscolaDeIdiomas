using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Aluno
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; } = new List<AlunosTurmas>();
    }
}
