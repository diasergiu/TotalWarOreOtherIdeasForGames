using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Barding : IModel_
    {
        public Barding()
        {
            Horses = new HashSet<Horse>();
        }
        [Column("IdBarding")]
        public int Id { get; set; }
        public int ArmorValue { get; set; }
        [Display(Name ="Barding Name")]
        public string BardingName { get; set; }

        public virtual ICollection<Horse> Horses { get; set; }
        
    }
}
