using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Interfaces
{
    public interface ITurmaRepository
    {
        ICollection<Turma> GetTodasTurmas(); // Achar todas as Turmas existentes
        Turma? GetTurmaPorId(int id); // Achar a Turma pelo Id
        ICollection<Turma> GetTurmaPorIdioma(string idioma); // Achar a Turma pelo idioma
        ICollection<Turma> GetTurmaPorNivel(string nivel); // Achar a Turma pelo nivel
        ICollection<Aluno> GetAlunoPorTurma(int turmaId); // Achar os alunos matriculados nessa turma
        bool TurmaExiste(int turmaId); // Saber se a turma existe ou não pelo Id
        bool CreateTurma(Turma turma); // Criar uma turma
        bool UpdateTurma(Turma turma); // Atualizar as informações de uma turma
        bool DeleteTurma(Turma turma); // Deletar uma turma que não tenha alunos
        int TemAluno(int turmaId); // Verificar se tem algum aluno na turma
        bool Save(); // Salvar
    }
}
