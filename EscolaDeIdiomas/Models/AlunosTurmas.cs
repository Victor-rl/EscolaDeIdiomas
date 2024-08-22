namespace EscolaDeIdiomas.Models
{
    public class AlunosTurmas
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }

    }
}
