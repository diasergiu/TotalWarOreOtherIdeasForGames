using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class SoldierModel
    {
        public SoldierModel()
        {
            SoldierFormations = new HashSet<SoldierFormation>();
        }

        public int IdSoldier { get; set; }
        public int AttackSkilll { get; set; }
        public int DefenceSkill { get; set; }
        public int Stamina { get; set; }
        public int Speed { get; set; }
        public int Acuracy { get; set; }
        public string SoldierName { get; set; }

        public virtual ICollection<SoldierFormation> SoldierFormations { get; set; }
    }
}
