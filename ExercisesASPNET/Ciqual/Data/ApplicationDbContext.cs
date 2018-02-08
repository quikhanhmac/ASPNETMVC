using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ciqual.Models;

namespace Ciqual.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Aliment> Aliment { get; set; }
        public virtual DbSet<Composition> Composition { get; set; }
        public virtual DbSet<Constituant> Constituant { get; set; }
        public virtual DbSet<Famille> Famille { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aliment>(entity =>
            {
                entity.HasKey(e => e.IdAliment);

                entity.Property(e => e.IdAliment).ValueGeneratedNever();

                entity.Property(e => e.CodeFamille)
                    .IsRequired()
                    .HasColumnType("char(4)");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CodeFamilleNavigation)
                    .WithMany(p => p.Aliment)
                    .HasForeignKey(d => d.CodeFamille)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Aliment_Famille_FK");
            });

            modelBuilder.Entity<Composition>(entity =>
            {
                entity.HasKey(e => new { e.IdAliment, e.IdConstituant });

                entity.Property(e => e.NoteConfiance).HasColumnType("char(1)");

                entity.Property(e => e.ValeurMoy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlimentNavigation)
                    .WithMany(p => p.Composition)
                    .HasForeignKey(d => d.IdAliment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Composition_Aliment_FK");

                entity.HasOne(d => d.IdConstituantNavigation)
                    .WithMany(p => p.Composition)
                    .HasForeignKey(d => d.IdConstituant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Composition_Constituant_FK");
            });

            modelBuilder.Entity<Constituant>(entity =>
            {
                entity.HasKey(e => e.IdConstituant);

                entity.Property(e => e.IdConstituant).ValueGeneratedNever();

                entity.Property(e => e.CodeEuroFir)
                    .HasColumnName("CodeEuroFIR")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Unite)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Famille>(entity =>
            {
                entity.HasKey(e => e.CodeFamille);

                entity.Property(e => e.CodeFamille)
                    .HasColumnType("char(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
