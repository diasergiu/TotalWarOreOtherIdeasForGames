using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarDLA.Models
{
    public class HorseBarding
    {
        public int IdHorse { get; set; }
        public int IdBarding { get; set; }

        public virtual Horse IdHorseNavigation { get; set; }

        public virtual Barding IdBardingNavigation { get; set; }
    }
}
