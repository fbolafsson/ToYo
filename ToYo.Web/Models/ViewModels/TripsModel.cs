using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToYo.Web.Models.ViewModels
{
    public class TripsModel
    {
        public IList<TripModel> Trips { get; set; }
        public DateTime JourneyTime { get; set; }
        public decimal? TotalPrice {
            get {
                return Trips.Any(x => x.Price == null) ? null : Trips.Sum(x => x.Price); 
            }
        }
    }
}