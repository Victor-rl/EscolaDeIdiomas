using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Repository
{
    public static class AlunoRepository
    {
        public static List<Aluno> Alunos { get; set; } = new List<Aluno>{
                new Aluno
            {
                Id = 1,
                Nome = "Jeff",
                Email = "Cav",
                CPF = "123",
                Ativo = false
            },
                new Aluno
            {
                Id = 2,
                Nome = "Po",
                Email = "Asd",
                CPF = "1445",
                Ativo = true
            }
        };
    }
}
