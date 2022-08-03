using System;
using System.Collections.Generic;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class FormationTrait : IJointModel
    {
        public FormationTrait()
        {

        }

        public FormationTrait(Formation formation, Trait trait)
        {
            this.IdFormationNavigation = formation;
            this.IdTraitNavigation = trait;
            this.IdTrait = trait.Id;
            this.IdFormation = formation.Id;
        }
        public int IdFormation { get; set; }
        public int IdTrait { get; set; }

        public virtual Formation IdFormationNavigation { get; set; }
        public virtual Trait IdTraitNavigation { get; set; }

        public override void saveYourself(TotalWarWanaBeContext context)
        {
            context.FormationTraits.Add(this);
        }
    }
}
