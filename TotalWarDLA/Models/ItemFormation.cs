using System;
using System.Collections.Generic;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class ItemFormation
    {
        public int IdItem { get; set; }
        public int IdFormation { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Formation IdFormationNavigation { get; set; }
    }
}
