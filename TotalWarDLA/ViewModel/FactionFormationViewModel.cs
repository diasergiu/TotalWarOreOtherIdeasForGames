using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarDLA.ViewModel
{
    public class FactionFormationViewModel
    {
        public Faction faction { get; set; }
        public IList<Formation> ListFormations { get; set; }

        public int[] IdFormations { get; set; }

    }
}
