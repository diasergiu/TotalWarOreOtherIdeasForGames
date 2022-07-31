using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.ViewModel
{
    //[ModelBinder(BinderType =(typeof(FactionModelBinder)))]
    public class FactionViewModel
    {
        public Faction Faction_ { get; set; }
        public IEnumerable<Formation> ListFormations { get; set; }

        public int[] Formations_ { get; set; }
    }
}
