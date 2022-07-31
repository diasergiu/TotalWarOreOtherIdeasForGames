using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.ViewModel
{
    public class ItemViewModel
    {
        public ItemViewModel()
        {
                
        }
        public ItemViewModel(Item item)
        {
            this.Item_ = item;
        }

        public Item Item_ {get;set;}
        public int[] Formations_ { get; set; }
    }
}
