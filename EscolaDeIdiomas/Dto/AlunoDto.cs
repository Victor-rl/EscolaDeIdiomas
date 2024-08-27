namespace EscolaDeIdiomas.Dto
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Nome do aluno
        public string CPF { get; set; } = string.Empty; // CPF do aluno
        public string Email { get; set; } = string.Empty; // Email do aluno
    }
}
