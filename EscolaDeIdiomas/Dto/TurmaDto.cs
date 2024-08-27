namespace EscolaDeIdiomas.Dto
{
    public class TurmaDto
    {
        public int Id { get; set; }
        public string Idioma { get; set; } = string.Empty; // O idioma da turma, ex: ingles, espanhol, portugues
        public string Nivel { get; set; } = string.Empty; // Se o idioma está no estágio basico, intermediario ou avancado
        public int Numero { get; set; } // O número da turma, ex: Turma 301 ou 105
        public int AnoLetivo { get; set; } // Ano em que se passa o curso, ex: 2005, 2020
    }
}
