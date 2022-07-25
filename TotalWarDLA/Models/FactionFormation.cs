﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class FactionFormation
    {

        //InvalidOperationException: No suitable constructor was found for entity type 'FactionFormation'. The following constructors had parameters that could not be bound to properties of the entity type: cannot bind 'faction', 'formation' in 'FactionFormation(Faction faction, Formation formation
       // so apperantly it needs normal construction in the faction controller 
        public FactionFormation()
        {

        }
        
        // in order to save do i need to just the int ID's ore do i need the object ass well
        public FactionFormation(Faction faction, Formation formation)
        {
            IdFaction = faction.IdFaction;
            IdFormation = formation.IdFormation;
            IdFormationNavigation = formation;
            IdFactionNavigation = faction;
        }
        public int IdFaction { get; set; }
        public int IdFormation { get; set; }

        public virtual Faction IdFactionNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }
    }
}