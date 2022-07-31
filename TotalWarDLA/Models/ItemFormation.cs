using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class ItemFormation
    {
        public ItemFormation()
        {

        }
        public ItemFormation(Item item_, Formation formation)
        {
            this.IdItem = item_.IdItem;
            this.IdFormation = formation.IdFormation;
            this.IdItemNavigation = item_;
            this.IdFormationNavigation = formation;
            
        }

        public int IdItem { get; set; }
        public int IdFormation { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }
    }
}
