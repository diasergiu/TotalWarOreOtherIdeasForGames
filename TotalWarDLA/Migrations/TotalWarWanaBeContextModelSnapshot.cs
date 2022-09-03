﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TotalWarDLA.Models;

namespace TotalWarDLA.Migrations
{
    [DbContext(typeof(TotalWarWanaBeContext))]
    partial class TotalWarWanaBeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TotalWarDLA.Models.Barding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdBarding")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArmorValue")
                        .HasColumnType("int");

                    b.Property<string>("BardingName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Bardings");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Faction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdFaction")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FactionDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FactionName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Factions");
                });

            modelBuilder.Entity("TotalWarDLA.Models.FactionFormation", b =>
                {
                    b.Property<int>("IdFaction")
                        .HasColumnType("int")
                        .HasColumnName("IdFaction");

                    b.Property<int>("IdFormation")
                        .HasColumnType("int")
                        .HasColumnName("IdFormation");

                    b.HasKey("IdFaction", "IdFormation");

                    b.HasIndex(new[] { "IdFormation" }, "IX_FactionFormation_FormationsIdFormation");

                    b.ToTable("FactionFormation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Formation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdFormation")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FormationName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IdHorse")
                        .HasColumnType("int");

                    b.Property<int?>("IdSoldier")
                        .HasColumnType("int");

                    b.Property<int>("NumberSoldiers")
                        .HasColumnType("int");

                    b.Property<int>("StartingFormationValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdHorse" }, "IX_Formations_IdHorse");

                    b.HasIndex(new[] { "IdSoldier" }, "IX_Formations_IdSoldier");

                    b.ToTable("Formations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.FormationTrait", b =>
                {
                    b.Property<int>("IdLeft")
                        .HasColumnType("int")
                        .HasColumnName("IdFormation");

                    b.Property<int>("IdRight")
                        .HasColumnType("int")
                        .HasColumnName("IdTrait");

                    b.HasKey("IdLeft", "IdRight");

                    b.HasIndex(new[] { "IdRight" }, "IX_FormationTrait_TraitsIdTrait");

                    b.ToTable("FormationTrait");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Horse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdHorse")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttackModifier")
                        .HasColumnType("int");

                    b.Property<string>("BreedName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("DefenceModifiered")
                        .HasColumnType("int");

                    b.Property<int>("HorseStamina")
                        .HasColumnType("int");

                    b.Property<int>("HorseStrength")
                        .HasColumnType("int");

                    b.Property<int?>("IdBarding")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdBarding" }, "IX_Horses_IdBarding");

                    b.ToTable("Horses");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdItem")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ItemName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SpeedCost")
                        .HasColumnType("int");

                    b.Property<int>("StaminaCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TotalWarDLA.Models.ItemFormation", b =>
                {
                    b.Property<int>("IdItem")
                        .HasColumnType("int")
                        .HasColumnName("IdItem");

                    b.Property<int>("IdFormation")
                        .HasColumnType("int")
                        .HasColumnName("IdFormation");

                    b.HasKey("IdItem", "IdFormation");

                    b.HasIndex(new[] { "IdFormation" }, "IX_ItemFormation_FormationsIdFormation");

                    b.ToTable("ItemFormation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.SoldierModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdSoldier")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Acuracy")
                        .HasColumnType("int");

                    b.Property<int>("AttackSkilll")
                        .HasColumnType("int");

                    b.Property<int>("DefenceSkill")
                        .HasColumnType("int");

                    b.Property<string>("SoldierName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Stamina")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SoldierModels");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Trait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdTrait")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TraitDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TraitName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Traits");
                });

            modelBuilder.Entity("TotalWarDLA.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TotalWarDLA.Models.FactionFormation", b =>
                {
                    b.HasOne("TotalWarDLA.Models.Faction", "IdFactionNavigation")
                        .WithMany("FactionFormations")
                        .HasForeignKey("IdFaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TotalWarDLA.Models.Formation", "IdFormationNavigation")
                        .WithMany("FactionFormations")
                        .HasForeignKey("IdFormation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdFactionNavigation");

                    b.Navigation("IdFormationNavigation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Formation", b =>
                {
                    b.HasOne("TotalWarDLA.Models.Horse", "IdHorseNavigation")
                        .WithMany("Formations")
                        .HasForeignKey("IdHorse")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TotalWarDLA.Models.SoldierModel", "IdSoldierNavigation")
                        .WithMany("Formations")
                        .HasForeignKey("IdSoldier")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IdHorseNavigation");

                    b.Navigation("IdSoldierNavigation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.FormationTrait", b =>
                {
                    b.HasOne("TotalWarDLA.Models.Formation", "IdFormationNavigation")
                        .WithMany("FormationTraits")
                        .HasForeignKey("IdLeft")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TotalWarDLA.Models.Trait", "IdTraitNavigation")
                        .WithMany("FormationTraits")
                        .HasForeignKey("IdRight")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdFormationNavigation");

                    b.Navigation("IdTraitNavigation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Horse", b =>
                {
                    b.HasOne("TotalWarDLA.Models.Barding", "IdBardingNavigation")
                        .WithMany("Horses")
                        .HasForeignKey("IdBarding")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IdBardingNavigation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.ItemFormation", b =>
                {
                    b.HasOne("TotalWarDLA.Models.Formation", "IdFormationNavigation")
                        .WithMany("ItemFormations")
                        .HasForeignKey("IdFormation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TotalWarDLA.Models.Item", "IdItemNavigation")
                        .WithMany("ItemFormations")
                        .HasForeignKey("IdItem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdFormationNavigation");

                    b.Navigation("IdItemNavigation");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Barding", b =>
                {
                    b.Navigation("Horses");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Faction", b =>
                {
                    b.Navigation("FactionFormations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Formation", b =>
                {
                    b.Navigation("FactionFormations");

                    b.Navigation("FormationTraits");

                    b.Navigation("ItemFormations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Horse", b =>
                {
                    b.Navigation("Formations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Item", b =>
                {
                    b.Navigation("ItemFormations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.SoldierModel", b =>
                {
                    b.Navigation("Formations");
                });

            modelBuilder.Entity("TotalWarDLA.Models.Trait", b =>
                {
                    b.Navigation("FormationTraits");
                });
#pragma warning restore 612, 618
        }
    }
}
