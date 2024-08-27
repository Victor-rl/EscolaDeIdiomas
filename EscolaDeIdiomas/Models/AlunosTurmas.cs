﻿namespace EscolaDeIdiomas.Models
{
    public class AlunosTurmas
    {
        public int AlunoId { get; set; } // Pega o Id do Aluno para fazer uma joint table
        public int TurmaId { get; set; } // Pega o Id da Turma para fazer uma joint table
        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }

    }
}
