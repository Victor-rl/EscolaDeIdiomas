using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Aluno
    {
        public int Id {  get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; }
    }
}
