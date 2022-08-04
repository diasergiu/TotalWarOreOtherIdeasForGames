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
        public int IdLeft { get; set; }
        [ForeignKey("IdTraitNavigation")]
        [Column("IdTrait")]
        public int IdRight { get; set; }

        public virtual Formation IdFormationNavigation { get; set; }
        public virtual Trait IdTraitNavigation { get; set; }
        #region "Get_set_IJoinModel" 
        public IModel_ GetIdNavigationLeftModel()
        {
            return this.IdFormationNavigation;
        }

        public IModel_ GetIdNavigationRightModel()
        {
            return this.IdTraitNavigation;
        }
        public void SetIdNavigationLeftModel(IModel_ modelLeft)
        {
            this.IdFormationNavigation = (Formation)modelLeft;
        }

        public void SetIdNavigationRightModel(IModel_ modelRight)
        {
            this.IdTraitNavigation = (Trait)modelRight;
        }
        #endregion

        public void saveYourself(TotalWarWanaBeContext context)
        {
            context.FormationTraits.Add(this);
        }
    }

        
}

