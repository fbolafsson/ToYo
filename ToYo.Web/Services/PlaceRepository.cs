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
    public class PlaceRepository
    {
        private IList<Place> places;

        public PlaceRepository()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToYo.Web.Places.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                places = JsonConvert.DeserializeObject<List<Place>>(result);
            }
        }

        public IList<Place> GetPlaces()
        {
            return places;
        }

        public Place Get(int id)
        {
            return places.Single(x => x.Id == id);
        }
    }
}