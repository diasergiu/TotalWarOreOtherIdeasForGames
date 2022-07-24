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
        public virtual DbSet<FactionSoldierFormation> FactionSoldierFormations { get; set; }
        public virtual DbSet<Horse> Horses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemSoldierFormation> ItemSoldierFormations { get; set; }
        public virtual DbSet<SoldierFormation> SoldierFormations { get; set; }
        public virtual DbSet<SoldierFormationTrait> SoldierFormationTraits { get; set; }
        public virtual DbSet<SoldierModel> SoldierModels { get; set; }
        public virtual DbSet<Trait> Traits { get; set; }

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
                entity.HasKey(e => e.IdBarding);

                entity.Property(e => e.BardingName).HasMaxLength(255);
            });

            modelBuilder.Entity<Faction>(entity =>
            {
                entity.HasKey(e => e.IdFaction);

                entity.Property(e => e.FactionDescription).HasMaxLength(255);

                entity.Property(e => e.FactionName).HasMaxLength(255);
            });

            modelBuilder.Entity<FactionSoldierFormation>(entity =>
            {
                entity.HasKey(e => new { e.FactionsIdFaction, e.SoldierFormationsIdFormation });

                entity.ToTable("FactionSoldierFormation");

                entity.HasIndex(e => e.SoldierFormationsIdFormation, "IX_FactionSoldierFormation_SoldierFormationsIdFormation");

                entity.HasOne(d => d.FactionsIdFactionNavigation)
                    .WithMany(p => p.FactionSoldierFormations)
                    .HasForeignKey(d => d.FactionsIdFaction);

                entity.HasOne(d => d.SoldierFormationsIdFormationNavigation)
                    .WithMany(p => p.FactionSoldierFormations)
                    .HasForeignKey(d => d.SoldierFormationsIdFormation);
            });

            modelBuilder.Entity<Horse>(entity =>
            {
                entity.HasKey(e => e.IdHorse);

                entity.HasIndex(e => e.BardingIdBarding, "IX_Horses_BardingIdBarding");

                entity.Property(e => e.BreedName).HasMaxLength(255);

                entity.HasOne(d => d.BardingIdBardingNavigation)
                    .WithMany(p => p.Horses)
                    .HasForeignKey(d => d.BardingIdBarding);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.Property(e => e.ItemName).HasMaxLength(255);
            });

            modelBuilder.Entity<ItemSoldierFormation>(entity =>
            {
                entity.HasKey(e => new { e.ItemsIdItem, e.SoldierFormationsIdFormation });

                entity.ToTable("ItemSoldierFormation");

                entity.HasIndex(e => e.SoldierFormationsIdFormation, "IX_ItemSoldierFormation_SoldierFormationsIdFormation");

                entity.HasOne(d => d.ItemsIdItemNavigation)
                    .WithMany(p => p.ItemSoldierFormations)
                    .HasForeignKey(d => d.ItemsIdItem);

                entity.HasOne(d => d.SoldierFormationsIdFormationNavigation)
                    .WithMany(p => p.ItemSoldierFormations)
                    .HasForeignKey(d => d.SoldierFormationsIdFormation);
            });

            modelBuilder.Entity<SoldierFormation>(entity =>
            {
                entity.HasKey(e => e.IdFormation);

                entity.HasIndex(e => e.HorseIdHorse, "IX_SoldierFormations_HorseIdHorse");

                entity.HasIndex(e => e.SoldierModelIdSoldier, "IX_SoldierFormations_SoldierModelIdSoldier");

                entity.Property(e => e.FormationName).HasMaxLength(255);

                entity.HasOne(d => d.HorseIdHorseNavigation)
                    .WithMany(p => p.SoldierFormations)
                    .HasForeignKey(d => d.HorseIdHorse);

                entity.HasOne(d => d.SoldierModelIdSoldierNavigation)
                    .WithMany(p => p.SoldierFormations)
                    .HasForeignKey(d => d.SoldierModelIdSoldier);
            });

            modelBuilder.Entity<SoldierFormationTrait>(entity =>
            {
                entity.HasKey(e => new { e.SoldierFormationIdFormation, e.TraitsIdTrait });

                entity.ToTable("SoldierFormationTrait");

                entity.HasIndex(e => e.TraitsIdTrait, "IX_SoldierFormationTrait_TraitsIdTrait");

                entity.HasOne(d => d.SoldierFormationIdFormationNavigation)
                    .WithMany(p => p.SoldierFormationTraits)
                    .HasForeignKey(d => d.SoldierFormationIdFormation);

                entity.HasOne(d => d.TraitsIdTraitNavigation)
                    .WithMany(p => p.SoldierFormationTraits)
                    .HasForeignKey(d => d.TraitsIdTrait);
            });

            modelBuilder.Entity<SoldierModel>(entity =>
            {
                entity.HasKey(e => e.IdSoldier);

                entity.Property(e => e.SoldierName).HasMaxLength(255);
            });

            modelBuilder.Entity<Trait>(entity =>
            {
                entity.HasKey(e => e.IdTrait);

                entity.Property(e => e.TraitDescription).HasMaxLength(255);

                entity.Property(e => e.TraitName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
