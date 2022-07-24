using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class SoldierFormationTrait
    {
        public int SoldierFormationIdFormation { get; set; }
        public int TraitsIdTrait { get; set; }

        public virtual SoldierFormation SoldierFormationIdFormationNavigation { get; set; }
        public virtual Trait TraitsIdTraitNavigation { get; set; }
    }
}
