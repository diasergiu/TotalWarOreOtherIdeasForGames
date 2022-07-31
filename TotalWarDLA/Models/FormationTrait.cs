using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class FormationTrait
    {
        public FormationTrait()
        {

        }

        public FormationTrait(Formation formation, Trait trait)
        {
            this.IdFormationNavigation = formation;
            this.IdTraitNavigation = trait;
            this.IdTrait = trait.IdTrait;
            this.IdFormation = formation.IdFormation;
        }
        public int IdFormation { get; set; }
        public int IdTrait { get; set; }

        public virtual Formation IdFormationNavigation { get; set; }
        public virtual Trait IdTraitNavigation { get; set; }
    }
}
