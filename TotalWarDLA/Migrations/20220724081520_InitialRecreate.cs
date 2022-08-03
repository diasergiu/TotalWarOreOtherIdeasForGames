using Microsoft.EntityFrameworkCore.Migrations;

namespace TotalWarDLA.Migrations
{
    public partial class InitialRecreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bardings",
                columns: table => new
                {
                    IdBarding = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmorValue = table.Column<int>(type: "int", nullable: false),
                    BardingName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bardings", x => x.IdBarding);
                });

            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    IdFaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FactionDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.IdFaction);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaminaCost = table.Column<int>(type: "int", nullable: false),
                    SpeedCost = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.IdItem);
                });

            migrationBuilder.CreateTable(
                name: "SoldierModels",
                columns: table => new
                {
                    IdSoldier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackSkilll = table.Column<int>(type: "int", nullable: false),
                    DefenceSkill = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Acuracy = table.Column<int>(type: "int", nullable: false),
                    SoldierName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierModels", x => x.IdSoldier);
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    IdTrait = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraitDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TraitName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.IdTrait);
                });

            migrationBuilder.CreateTable(
                name: "Horses",
                columns: table => new
                {
                    IdHorse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackModifier = table.Column<int>(type: "int", nullable: false),
                    BreedName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefenceModifiered = table.Column<int>(type: "int", nullable: false),
                    HorseStamina = table.Column<int>(type: "int", nullable: false),
                    HorseStrength = table.Column<int>(type: "int", nullable: false),
                    IdBarding = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.IdHorse);
                    table.ForeignKey(
                        name: "FK_Horses_Bardings_IdBarding",
                        column: x => x.IdBarding,
                        principalTable: "Bardings",
                        principalColumn: "IdBarding",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberSoldiers = table.Column<int>(type: "int", nullable: false),
                    StartingFormationValue = table.Column<int>(type: "int", nullable: false),
                    FormationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdSoldier = table.Column<int>(type: "int", nullable: true),
                    IdHorse = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.IdFormation);
                    table.ForeignKey(
                        name: "FK_Formations_Horses_IdHorse",
                        column: x => x.IdHorse,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Formations_SoldierModels_IdSoldier",
                        column: x => x.IdSoldier,
                        principalTable: "SoldierModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactionFormation",
                columns: table => new
                {
                    IdFaction = table.Column<int>(type: "int", nullable: false),
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactionFormation", x => new { x.IdFaction, x.IdFormation });
                    table.ForeignKey(
                        name: "FK_FactionFormation_Factions_IdFaction",
                        column: x => x.IdFaction,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactionFormation_Formations_IdFormation",
                        column: x => x.IdFormation,
                        principalTable: "Formations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormationTrait",
                columns: table => new
                {
                    IdFormation = table.Column<int>(type: "int", nullable: false),
                    IdTrait = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormationTrait", x => new { x.IdFormation, x.IdTrait });
                    table.ForeignKey(
                        name: "FK_FormationTrait_Formations_IdFormation",
                        column: x => x.IdFormation,
                        principalTable: "Formations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormationTrait_Traits_IdTrait",
                        column: x => x.IdTrait,
                        principalTable: "Traits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemFormation",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false),
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFormation", x => new { x.IdItem, x.IdFormation });
                    table.ForeignKey(
                        name: "FK_ItemFormation_Formations_IdFormation",
                        column: x => x.IdFormation,
                        principalTable: "Formations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemFormation_Items_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactionFormation_FormationsIdFormation",
                table: "FactionFormation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_IdHorse",
                table: "Formations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_IdSoldier",
                table: "Formations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FormationTrait_TraitsIdTrait",
                table: "FormationTrait",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Horses_IdBarding",
                table: "Horses",
                column: "IdBarding");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFormation_FormationsIdFormation",
                table: "ItemFormation",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactionFormation");

            migrationBuilder.DropTable(
                name: "FormationTrait");

            migrationBuilder.DropTable(
                name: "ItemFormation");

            migrationBuilder.DropTable(
                name: "Factions");

            migrationBuilder.DropTable(
                name: "Traits");

            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Horses");

            migrationBuilder.DropTable(
                name: "SoldierModels");

            migrationBuilder.DropTable(
                name: "Bardings");
        }
    }
}
