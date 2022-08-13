using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class SoldierModel :IModel_
    {
        public SoldierModel()
        {
            Formations = new HashSet<Formation>();
        }
        [Column("IdSoldier")]
        public int Id { get; set; }
        public int AttackSkilll { get; set; }
        public int DefenceSkill { get; set; }
        public int Stamina { get; set; }
        public int Speed { get; set; }
        public int Acuracy { get; set; }
        public string SoldierName { get; set; }

        public virtual ICollection<Formation> Formations { get; set; }
    }
}
