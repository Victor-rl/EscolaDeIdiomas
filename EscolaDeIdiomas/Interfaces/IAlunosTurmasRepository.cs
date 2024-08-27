using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Interfaces
{
    public interface IAlunosTurmasRepository
    {
        int QuantidadeDeAluno(int turmaId); // Quantifica os alunos em uma turma
        bool MatricularAlunoNaTurma(AlunosTurmas alunosTurmas); // Matricula o aluno na turma
        bool VerificarMatricula(int alunoId, int turmaId); // Verifica se o aluno já está matriculado na turma
        bool DesvincularAlunoTurma(AlunosTurmas alunosTurmas); // Desvincula o aluno da turma
        bool Save(); // Salvar
    }
}
