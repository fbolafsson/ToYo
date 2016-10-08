using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToYo.Web.Models
{
    public class Trip
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string DepartureTime { get; set; }
        public TimeSpan Departure { get
            {
                if (string.IsNullOrEmpty(DepartureTime))
                {
                    return new TimeSpan();
                }
                var departure = DepartureTime.Split(':');
                return new TimeSpan(Convert.ToInt32(departure[0]), Convert.ToInt32(departure[1]), 0);
            }
        }
        public string ArrivalTime { get; set; }
        public TimeSpan Arrival {
            get
            {

                if (string.IsNullOrEmpty(ArrivalTime))
                {
                    return new TimeSpan();
                }
                var arrival = ArrivalTime.Split(':');
                return new TimeSpan(Convert.ToInt32(arrival[0]), Convert.ToInt32(arrival[1]), 0);
            }
        }

        public DateTime Date { get; set; }

        public int AgentId { get; set; }

        public decimal? Price { get; set; }
    }
}