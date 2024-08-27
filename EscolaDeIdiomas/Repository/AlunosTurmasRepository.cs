using AutoMapper;
using EscolaDeIdiomas.Data;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeIdiomas.Repository
{
    public class AlunosTurmasRepository : IAlunosTurmasRepository
    {
        private readonly DataContexto _contexto;
        private readonly IMapper _mapper;

        public AlunosTurmasRepository(DataContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public bool MatricularAlunoNaTurma(AlunosTurmas alunosTurmas)  // Matricula o aluno na turma
        {
            _contexto.Add(alunosTurmas);

            return Save();
        }

        public bool Save() // Salvar
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }

        public int QuantidadeDeAluno(int turmaId) // Quantifica os alunos em uma turma
        {
            return _contexto.AlunosTurmas.Count(at => at.TurmaId == turmaId);
        }

        public bool VerificarMatricula(int alunoId, int turmaId) // Verifica se o aluno já está matriculado na turma
        {
            return _contexto.AlunosTurmas.Any(at => at.AlunoId == alunoId && at.TurmaId == turmaId);
        }

        public bool DesvincularAlunoTurma(AlunosTurmas alunosTurmas) // Desvincula o aluno da turma
        {
            _contexto.Remove(alunosTurmas);
            return Save();
        }
    }
}
