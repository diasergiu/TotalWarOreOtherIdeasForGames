using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemSoldierFormations = new HashSet<ItemSoldierFormation>();
        }

        public int IdItem { get; set; }
        public int StaminaCost { get; set; }
        public int SpeedCost { get; set; }
        public string ItemName { get; set; }

        public virtual ICollection<ItemSoldierFormation> ItemSoldierFormations { get; set; }
    }
}
