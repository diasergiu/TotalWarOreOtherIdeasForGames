using System;
using System.Collections.Generic;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class ItemFormation :IJointModel
    {
        public ItemFormation()
        {

        }
        public ItemFormation(Item item_, Formation formation)
        {
            this.IdItem = item_.Id;
            this.IdFormation = formation.Id;
            this.IdItemNavigation = item_;
            this.IdFormationNavigation = formation;
            
        }

        public int IdItem { get; set; }
        public int IdFormation { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }

        public override void saveYourself(TotalWarWanaBeContext context)
        {
            context.ItemFormations.Add(this);
        }
    }
}
