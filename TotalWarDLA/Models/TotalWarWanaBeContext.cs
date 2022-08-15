using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class TotalWarWanaBeContext : DbContext
    {
        public TotalWarWanaBeContext()
        {
        }

        public TotalWarWanaBeContext(DbContextOptions<TotalWarWanaBeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Barding> Bardings { get; set; }
        public virtual DbSet<Faction> Factions { get; set; }
        public virtual DbSet<FactionFormation> FactionFormations { get; set; }
        public virtual DbSet<Horse> Horses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemFormation> ItemFormations { get; set; }
        public virtual DbSet<Formation> Formations { get; set; }
        public virtual DbSet<FormationTrait> FormationTraits { get; set; }
        public virtual DbSet<SoldierModel> SoldierModels { get; set; }
        public virtual DbSet<Trait> Traits { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=TotalWarWanaBe;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Barding>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BardingName).HasMaxLength(255);
            });

            modelBuilder.Entity<Faction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FactionDescription).HasMaxLength(255);

                entity.Property(e => e.FactionName).HasMaxLength(255);
            });

            modelBuilder.Entity<FactionFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdFaction, e.IdFormation });

                entity.ToTable("FactionFormation");

                entity.HasIndex(e => e.IdFormation, "IX_FactionFormation_FormationsIdFormation");

                entity.HasOne(d => d.IdFactionNavigation)
                    .WithMany(p => p.FactionFormations)
                    .HasForeignKey(d => d.IdFaction)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdFormationNavigation)
                    .WithMany(p => p.FactionFormations)
                    .HasForeignKey(d => d.IdFormation)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Horse>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.IdBarding, "IX_Horses_IdBarding");

                entity.Property(e => e.BreedName).HasMaxLength(255);

                entity.HasOne(d => d.IdBardingNavigation)
                    .WithMany(p => p.Horses)
                    .HasForeignKey(d => d.IdBarding)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ItemName).HasMaxLength(255);
                entity.Property(e => e.Image);
            });

            modelBuilder.Entity<ItemFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdItem, e.IdFormation });

                entity.ToTable("ItemFormation");

                entity.HasIndex(e => e.IdFormation, "IX_ItemFormation_FormationsIdFormation");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.ItemFormations)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdFormationNavigation)
                    .WithMany(p => p.ItemFormations)
                    .HasForeignKey(d => d.IdFormation)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Formation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.IdHorse, "IX_Formations_IdHorse" /*what are thowse at the end */);

                entity.HasIndex(e => e.IdSoldier, "IX_Formations_IdSoldier");

                entity.Property(e => e.FormationName).HasMaxLength(255);

                entity.HasOne(d => d.IdHorseNavigation)
                    .WithMany(p => p.Formations)
                    .HasForeignKey(d => d.IdHorse)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.IdSoldierNavigation)
                    .WithMany(p => p.Formations)
                    .HasForeignKey(d => d.IdSoldier)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FormationTrait>(entity =>
            {
                entity.HasKey(e => new { e.IdLeft, e.IdRight });

                entity.ToTable("FormationTrait");

                entity.HasIndex(e => e.IdRight, "IX_FormationTrait_TraitsIdTrait");

                entity.HasOne(d => d.IdFormationNavigation)
                    .WithMany(p => p.FormationTraits)
                    .HasForeignKey(d => d.IdLeft)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdTraitNavigation)
                    .WithMany(p => p.FormationTraits)
                    .HasForeignKey(d => d.IdRight)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SoldierModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.SoldierName).HasMaxLength(255);
            });

            modelBuilder.Entity<Trait>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TraitDescription).HasMaxLength(255);

                entity.Property(e => e.TraitName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }
           

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
