using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Interfaces
{
    public interface IAlunoRepository
    {
        ICollection<Aluno> GetTodosAlunos(); // Achar todos os alunos existentes
        Aluno? GetAlunoPorId(int id); // Achar aluno pela Id
        Aluno? GetAlunoPorNome(string nome); // Achar aluno pelo nome
        ICollection<Turma> GetTurmasMatriculadasDoAluno(int alunoId); // Achar as turmas que o aluno está matriculado
        bool AlunoExiste(int alunoId); // Saber se o aluno existe
        bool CreateAluno(int turmarId, Aluno aluno); // Criar um aluno e cadastra-lo a uma turma
        bool UpdateAluno(Aluno aluno); // Atualizar as informações de Nome, CPF e Email do aluno
        bool DeleteAluno(Aluno aluno); // Deletar o aluno que esteja sem turma
        int TemTurma(int alunoId); // Verificador se o aluno está em uma turma
        bool Save(); // Salvar as informações
    }
}
