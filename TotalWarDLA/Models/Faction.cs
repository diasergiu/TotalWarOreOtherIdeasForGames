using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Faction : IModel_
    {
        public Faction()
        {
            FactionFormations = new HashSet<FactionFormation>();
        }
        [Column("IdFaction")]
        public int Id { get; set; }
        public string FactionName { get; set; }
        public string FactionDescription { get; set; }

        public virtual ICollection<FactionFormation> FactionFormations { get; set; }
    }
}
