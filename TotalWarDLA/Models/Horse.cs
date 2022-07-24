using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Horse
    {
        public Horse()
        {
            Formations = new HashSet<Formation>();
        }

        public int IdHorse { get; set; }
        public int AttackModifier { get; set; }
        public string BreedName { get; set; }
        public int DefenceModifiered { get; set; }
        public int HorseStamina { get; set; }
        public int HorseStrength { get; set; }
        public int? IdBarding { get; set; }

        public virtual Barding IdBardingNavigation { get; set; }
        public virtual ICollection<Formation> Formations { get; set; }
    }
}
