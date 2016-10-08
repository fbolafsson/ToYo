using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using ToYo.Web.Models;

namespace ToYo.Web.Services
{
    public class TripRepository
    {
        private IList<Trip> trips;

        public TripRepository()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToYo.Web.Trips.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                trips = JsonConvert.DeserializeObject<List<Trip>>(result);
            }
        }

        public IList<Trip> GetTrips()
        {
            return trips;
        }
    }
}