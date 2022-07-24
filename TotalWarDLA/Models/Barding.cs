using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Barding
    {
        public Barding()
        {
            Horses = new HashSet<Horse>();
        }

        public int IdBarding { get; set; }
        public int ArmorValue { get; set; }
        [Display(Name ="Barding Name")]
        public string BardingName { get; set; }

        public virtual ICollection<Horse> Horses { get; set; }
    }
}
