using Locadora.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class LocadoraContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public LocadoraContext(DbContextOptions<LocadoraContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cliente>().Property(c => c.Nome)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode(false);
            modelBuilder.Entity<Cliente>().Property(c => c.Cpf)
                .HasMaxLength(11)
                .IsRequired()
                .IsUnicode(false);

        }
    }
}
