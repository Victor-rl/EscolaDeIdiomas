using EscolaDeIdiomas.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeIdiomas.Data
{
    public class DataContexto : DbContext
    {
        public DataContexto(DbContextOptions<DataContexto> opcoes) : base(opcoes) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunosTurmas> AlunosTurmas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunosTurmas>()
                .HasKey(at => new { at.TurmaId, at.AlunoId });

            modelBuilder.Entity<AlunosTurmas>()
                .HasOne(a => a.Aluno)
                .WithMany(at => at.AlunosTurmas)
                .HasForeignKey(t => t.AlunoId);

            modelBuilder.Entity<AlunosTurmas>()
                .HasOne(t => t.Turma)
                .WithMany(at => at.AlunosTurmas)
                .HasForeignKey(t => t.TurmaId);
        }
    }
}
