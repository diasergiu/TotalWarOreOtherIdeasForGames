using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Horse
    {
        public Horse()
        {
            SoldierFormations = new HashSet<SoldierFormation>();
        }

        public int IdHorse { get; set; }
        public int AttackModifier { get; set; }
        public string BreedName { get; set; }
        public int DefenceModifiered { get; set; }
        public int HorseStamina { get; set; }
        public int HorseStrength { get; set; }
        public int? BardingIdBarding { get; set; }

        public virtual Barding BardingIdBardingNavigation { get; set; }
        public virtual ICollection<SoldierFormation> SoldierFormations { get; set; }
    }
}
