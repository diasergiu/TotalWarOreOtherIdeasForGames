using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Faction
    {
        public Faction()
        {
            FactionSoldierFormations = new HashSet<FactionSoldierFormation>();
        }

        public int IdFaction { get; set; }
        public string FactionName { get; set; }
        public string FactionDescription { get; set; }

        public virtual ICollection<FactionSoldierFormation> FactionSoldierFormations { get; set; }
    }
}
