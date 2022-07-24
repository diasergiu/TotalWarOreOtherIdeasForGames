using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Trait
    {
        public Trait()
        {
            SoldierFormationTraits = new HashSet<SoldierFormationTrait>();
        }

        public int IdTrait { get; set; }
        public string TraitDescription { get; set; }
        public string TraitName { get; set; }

        public virtual ICollection<SoldierFormationTrait> SoldierFormationTraits { get; set; }
    }
}
