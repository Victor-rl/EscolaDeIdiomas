using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty; // Nome do aluno

        [Required]
        [MaxLength(15)]
        public string CPF { get; set; } = string.Empty; // CPF do aluno

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Email do aluno
        public bool Ativo { get; set; } = true; // Status se o aluno está matriculado ou não (Não implementado)
        [Required]
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; } = new List<AlunosTurmas>();
    }
}
