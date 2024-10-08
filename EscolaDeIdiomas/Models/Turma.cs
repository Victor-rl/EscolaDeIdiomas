﻿using System.ComponentModel.DataAnnotations;

namespace EscolaDeIdiomas.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Idioma { get; set; } = string.Empty; // O idioma da turma, ex: ingles, espanhol, portugues

        [Required]
        [MaxLength(15)]
        public string Nivel { get; set; } = string.Empty; // Se o idioma está no estágio basico, intermediario ou avancado

        [Required]
        [MaxLength(10)]
        public int Numero { get; set; } // O número da turma, ex: Turma 301 ou 105

        [Required]
        [MaxLength(4)]
        public int AnoLetivo { get; set; } // Ano em que se passa o curso, ex: 2005, 2020
        public ICollection<AlunosTurmas> AlunosTurmas { get; set; } = new List<AlunosTurmas>();
    }
}
