using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Turma
    {
        public int Id { get; set; }
        [Required]
        public string Idioma { get; set; }
        [Required]
        public string Nivel { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public int AnoLetivo { get; set; }
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; }
    }
}
