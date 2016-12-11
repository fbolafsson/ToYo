using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitSeydisfjordurModule
{
    public class Schedule
    {
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public decimal Price { get; set; }
        public Dictionary<string, List<string>> Dictionary { get; set; }
    }
}
