using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Formation
    {
        public Formation()
        {
            FactionFormations = new HashSet<FactionFormation>();
            ItemFormations = new HashSet<ItemFormation>();
            FormationTraits = new HashSet<FormationTrait>();
        }

        public int IdFormation { get; set; }
        public int NumberSoldiers { get; set; }
        public int StartingFormationValue { get; set; }
        public string FormationName { get; set; }
        public int? IdSoldier { get; set; }
        public int? IdHorse { get; set; }

        public virtual Horse IdHorseNavigation { get; set; }
        public virtual SoldierModel IdSoldierNavigation { get; set; }
        public virtual ICollection<FactionFormation> FactionFormations { get; set; }
        public virtual ICollection<ItemFormation> ItemFormations { get; set; }
        public virtual ICollection<FormationTrait> FormationTraits { get; set; }
    }
}
