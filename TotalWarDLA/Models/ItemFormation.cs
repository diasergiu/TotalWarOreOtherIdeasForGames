using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

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
            this.IdItem = item_.Id;
            this.IdFormation = formation.Id;
            this.IdItemNavigation = item_;
            this.IdFormationNavigation = formation;
            
        }
        [Column("IdItem")]
        public int IdItem { get; set; }
        [Column("IdFormation")]
        public int IdFormation { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }

        
    }
}
