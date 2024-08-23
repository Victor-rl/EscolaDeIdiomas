using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Idioma { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string Nivel { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public int Numero { get; set; }

        [Required]
        [MaxLength(4)]
        public int AnoLetivo { get; set; }
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; } = new List<AlunosTurmas>();
    }
}
