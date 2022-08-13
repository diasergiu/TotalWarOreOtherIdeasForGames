using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Horse :IModel_
    {
        public Horse()
        {
            Formations = new HashSet<Formation>();
        }
        [Column("IdHorse")]
        public int Id { get; set; }
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
