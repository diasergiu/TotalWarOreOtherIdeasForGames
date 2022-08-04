using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.IdRight = trait.Id;
            this.IdLeft = formation.Id;
        }
        [ForeignKey("IdFormationNavigation")]
        [Column("IdFormation")]
        public override int IdLeft { get; set; }
        [ForeignKey("IdTraitNavigation")]
        [Column("IdTrait")]
        public override int IdRight { get; set; }

        public virtual Formation IdFormationNavigation { get; set; }
        public virtual Trait IdTraitNavigation { get; set; }

        public override void saveYourself(TotalWarWanaBeContext context)
        {
            context.FormationTraits.Add(this);
        }
    }
}
