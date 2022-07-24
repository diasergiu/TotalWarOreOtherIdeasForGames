using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Trait
    {
        public Trait()
        {
            FormationTraits = new HashSet<FormationTrait>();
        }

        public int IdTrait { get; set; }
        public string TraitDescription { get; set; }
        public string TraitName { get; set; }

        public virtual ICollection<FormationTrait> FormationTraits { get; set; }
    }
}
