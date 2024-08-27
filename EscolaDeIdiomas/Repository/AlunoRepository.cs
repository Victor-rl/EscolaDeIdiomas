using AutoMapper;
using EscolaDeIdiomas.Data;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DataContexto _contexto;
        private readonly IMapper _mapper;

        public AlunoRepository(DataContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public ICollection<Aluno> GetTodosAlunos() // Achar todos os alunos existentes
        {
            return _contexto.Alunos.OrderBy(a => a.Id).ToList();
        }

        public Aluno? GetAlunoPorId(int id) // Achar aluno pela Id
        {
            return _contexto.Alunos.FirstOrDefault(a => a.Id == id);
        }

        public Aluno? GetAlunoPorNome(string nome) // Achar aluno pelo nome
        {
            return _contexto.Alunos.FirstOrDefault(a => a.Nome == nome);
        }

        public bool AlunoExiste(int alunoId) // Saber se o aluno existe
        {
            return _contexto.Alunos.Any(a => a.Id.Equals(alunoId));
        }

        public ICollection<Turma> GetTurmasMatriculadasDoAluno(int alunoId) // Achar as turmas que o aluno está matriculado
        {
            return _contexto.AlunosTurmas.Where(t => t.AlunoId == alunoId).Select(t => t.Turma).ToList();
        }

        public bool CreateAluno(int turmaId, Aluno aluno) // Criar um aluno e cadastra-lo a uma turma
        {
            var turma = _contexto.Turmas.FirstOrDefault(t => t.Id == turmaId);

            var turmaMatriculada = new AlunosTurmas()
            {
                Turma = turma,
                Aluno = aluno,
            };

            _contexto.Add(turmaMatriculada);
            _contexto.Add(aluno);

            return Save();
        }

        public bool Save() // Salvar as informações
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }

        public bool UpdateAluno(Aluno aluno) // Atualizar as informações de Nome, CPF e Email do aluno
        {
            _contexto.Update(aluno);
            return Save();
        }

        public int TemTurma(int alunoId) // Verificador se o aluno está em uma turma
        {
            return _contexto.AlunosTurmas.Count(at => at.AlunoId == alunoId);
        }

        public bool DeleteAluno(Aluno aluno) // Deletar o aluno que esteja sem turma
        {
            _contexto.Remove(aluno);
            return Save();
        }
    }
}
