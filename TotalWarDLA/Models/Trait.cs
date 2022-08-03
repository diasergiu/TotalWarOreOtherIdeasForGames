using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Trait :IModel_
    {
        public Trait()
        {
            FormationTraits = new HashSet<FormationTrait>();
        }
        [Column("IdTrait")]
        public int Id { get; set; }
        public string TraitDescription { get; set; }
        public string TraitName { get; set; }

        public virtual ICollection<FormationTrait> FormationTraits { get; set; }
    }
}
