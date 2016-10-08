using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToYo.Web.Models.ViewModels
{
    public class TripModel
    {
        public Place From { get; set; }
        public Place To { get; set; }
        public Agent Agent { get; set; }
        public DateTime Departure {get;set;}
        public DateTime Arrival { get; set; }
    }
}