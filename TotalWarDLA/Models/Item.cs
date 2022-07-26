﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TotalWarDLA.Models.NonDataModels;
using TotalWarDLA.Models.Pagination;

#nullable disable

namespace TotalWarDLA.Models
{
    public partial class Item :IModel_
    {
        public Item()
        {
            ItemFormations = new HashSet<ItemFormation>();
        }
        [Column("IdItem")]
        public int Id { get; set; }
        public int StaminaCost { get; set; }
        public int SpeedCost { get; set; }
        public string ItemName { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<ItemFormation> ItemFormations { get; set; }
    }
}
