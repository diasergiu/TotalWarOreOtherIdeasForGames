using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class FactionSoldierFormation
    {
        public int FactionsIdFaction { get; set; }
        public int SoldierFormationsIdFormation { get; set; }

        public virtual Faction FactionsIdFactionNavigation { get; set; }
        public virtual SoldierFormation SoldierFormationsIdFormationNavigation { get; set; }
    }
}
