using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class ItemSoldierFormation
    {
        public int ItemsIdItem { get; set; }
        public int SoldierFormationsIdFormation { get; set; }

        public virtual Item ItemsIdItemNavigation { get; set; }
        public virtual SoldierFormation SoldierFormationsIdFormationNavigation { get; set; }
    }
}
