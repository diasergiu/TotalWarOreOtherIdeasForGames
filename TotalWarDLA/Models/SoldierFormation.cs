using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class SoldierFormation
    {
        public SoldierFormation()
        {
            FactionSoldierFormations = new HashSet<FactionSoldierFormation>();
            ItemSoldierFormations = new HashSet<ItemSoldierFormation>();
            SoldierFormationTraits = new HashSet<SoldierFormationTrait>();
        }

        public int IdFormation { get; set; }
        public int NumberSoldiers { get; set; }
        public int StartingFormationValue { get; set; }
        public string FormationName { get; set; }
        public int? SoldierModelIdSoldier { get; set; }
        public int? HorseIdHorse { get; set; }

        public virtual Horse HorseIdHorseNavigation { get; set; }
        public virtual SoldierModel SoldierModelIdSoldierNavigation { get; set; }
        public virtual ICollection<FactionSoldierFormation> FactionSoldierFormations { get; set; }
        public virtual ICollection<ItemSoldierFormation> ItemSoldierFormations { get; set; }
        public virtual ICollection<SoldierFormationTrait> SoldierFormationTraits { get; set; }
    }
}
