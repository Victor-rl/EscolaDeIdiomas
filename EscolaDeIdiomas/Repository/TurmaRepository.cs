using AutoMapper;
using EscolaDeIdiomas.Data;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly DataContexto _contexto;
        private readonly IMapper _mapper;

        public TurmaRepository(DataContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public ICollection<Aluno> GetAlunoPorTurma(int turmaId) // Achar os alunos matriculados nessa turma
        {
            return _contexto.AlunosTurmas.Where(t => t.TurmaId == turmaId).Select(t => t.Aluno).ToList();
        }

        public ICollection<Turma> GetTodasTurmas() // Achar todas as Turmas existentes
        {
            return _contexto.Turmas.OrderBy(t => t.Id).ToList();
        }

        public Turma? GetTurmaPorId(int id) // Achar a Turma pelo Id
        {
            return _contexto.Turmas.FirstOrDefault(t => t.Id == id);
        }

        public ICollection<Turma> GetTurmaPorIdioma(string idioma) // Achar a Turma pelo idioma
        {
            return _contexto.Turmas.Where(t => t.Idioma == idioma).ToList();
        }

        public ICollection<Turma> GetTurmaPorNivel(string nivel) // Achar a Turma pelo nivel
        {
            return _contexto.Turmas.Where(t => t.Nivel == nivel).ToList();
        }
        public bool TurmaExiste(int turmaId) // Saber se a turma existe ou não pelo Id
        {
            return _contexto.Turmas.Any(t => t.Id.Equals(turmaId));
        }

        public bool CreateTurma(Turma turma) // Criar uma turma
        {
            _contexto.Add(turma);

            return Save();
        }

        public bool Save() // Salvar
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }

        public bool UpdateTurma(Turma turma) // Atualizar as informações de uma turma
        {
            _contexto.Update(turma);
            return Save();
        }

        public bool DeleteTurma(Turma turma) // Deletar uma turma que não tenha alunos
        {
            _contexto.Remove(turma);
            return Save();
        }

        public int TemAluno(int turmaId) // Verificar se tem algum aluno na turma
        {
            return _contexto.AlunosTurmas.Count(at => at.TurmaId == turmaId);
        }
    }
}
