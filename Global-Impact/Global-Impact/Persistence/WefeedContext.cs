using Global_Impact.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Persistence
{
    public class WefeedContext : DbContext
    {
        public WefeedContext(DbContextOptions options) : base(options) {}
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<Ong> ONGs { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<DoacaoAlimento> DoacoesAlimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoacaoAlimento>().HasKey(p => new { p.DoacaoId, p.AlimentoId });

            modelBuilder.Entity<DoacaoAlimento>()
            .HasOne(p => p.Doacao)
            .WithMany(m => m.DoacoesAlimentos)
            .HasForeignKey(m => m.DoacaoId);

            modelBuilder.Entity<DoacaoAlimento>()
            .HasOne(p => p.Alimento)
            .WithMany(m => m.DoacoesAlimentos)
            .HasForeignKey(m => m.AlimentoId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
