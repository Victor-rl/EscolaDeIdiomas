using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Repository
{
    public class TurmaRepository
    {
        public static List<Turma> Turmas { get; set; } = new List<Turma>{
                new Turma
            {
                Id = 1,
                Idioma = "Ingles",
                Nivel = "Avançado",
                Numero = 305,
                AnoLetivo = 2009

            },
                new Turma
            {
                Id = 2,
                Idioma = "Japones",
                Nivel = "Basico",
                Numero = 102,
                AnoLetivo = 2020
            }
        };
    }
}
