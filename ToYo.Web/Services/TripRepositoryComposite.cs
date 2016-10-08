using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToYo.Web.Models;

namespace ToYo.Web.Services
{
    public class TripRepositoryComposite : ITripRepository
    {
        IList<ITripRepository> services;

        public TripRepositoryComposite(IList<ITripRepository> services)
        {
            this.services = services;
        }

        public IList<Trip> GetTrips(DateTime date)
        {
            var trips = new List<Trip>();
            foreach(var service in services)
            {
                trips.AddRange(service.GetTrips(date));
            }
            return trips;
        }
    }
}