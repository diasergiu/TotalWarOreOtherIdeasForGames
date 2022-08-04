using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class FactionFormation 
    {

        //InvalidOperationException: No suitable constructor was found for entity type 'FactionFormation'. The following constructors had parameters that could not be bound to properties of the entity type: cannot bind 'Faction_', 'formation' in 'FactionFormation(Faction Faction_, Formation formation
       // so apperantly it needs normal construction in the Faction_ controller 
        public FactionFormation()
        {

        }
        
        // in order to save do i need to just the int ID's ore do i need the object ass well
        public FactionFormation(Faction faction, Formation formation)
        {
            this.IdFaction = faction.Id;
            this.IdFormation = formation.Id;
            this.IdFormationNavigation = formation;
            this.IdFactionNavigation = faction;
        }
        [Column("IdFaction")]
        public int IdFaction { get; set; }
        [Column("IdFormation")]
        public int IdFormation { get; set; }

        public virtual Faction IdFactionNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }
        
    }
}
