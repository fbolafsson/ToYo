using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToYo.Web.Models;
using ToYo.Web.Models.ViewModels;
using ToYo.Web.Services;

namespace ToYo.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITripRepository tripRepository;
        private PlaceRepository placeRpository;
        private AgentRepository agentRpository;

        public HomeController()
        {
            tripRepository = new TripRepositoryComposite(new List<ITripRepository> { new TripJsonRepository(), new GrayLineTripRepository() });
            placeRpository = new PlaceRepository();
            agentRpository = new AgentRepository();
        }

        public ActionResult Index()
        {
            var model = new HomeModel {
                Places = placeRpository.GetPlaces()
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                Date = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        public ActionResult Trip(TripRequestModel model)
        {
            var trips = tripRepository.GetTrips(model.Date ?? DateTime.Today);
            var tails = trips.Where(x => x.FromId == model.From).Where(x => x.Date.Date == model.Date.Value.Date).Select(x => new List<Trip> { x }).ToList();

            var possibleRouts = new List<List<Trip>>();

            for(var i = 0; i < 7; i++)
            {
                tails = PopulateTail(tails, model.To, trips);
            }

            var routes = tails.Where(x => x.Last().ToId == model.To && x.Count() < 7).ToList().OrderBy(x => x.Count);
            
            var tripModel = routes.Select(CreateTripsModel)
                .Where(x => !x.Trips.Any(y => y.Departure < DateTime.Now))
                .OrderBy(x => x.JourneyTime).ToList();
            return View(tripModel);
        }

        private TripsModel CreateTripsModel(IList<Trip> trips)
        {
            var tripsModel = trips.Select(y =>
                new TripModel()
                {
                    From = placeRpository.Get(y.FromId),
                    To = placeRpository.Get(y.ToId),
                    Agent = agentRpository.GetAgent(y.AgentId),
                    Departure = y.Date.Add(y.Departure),
                    Arrival = y.Date.Add(y.Arrival),
                    Price = y.Price
                });
            var journeyTime = new DateTime().Add(trips.Last().Arrival.Subtract(trips.First().Departure));
            return new TripsModel
            {
                Trips = tripsModel.ToList(),
                JourneyTime = journeyTime
            };
        }

        private List<List<Trip>> PopulateTail(List<List<Trip>> tails, int destination, IList<Trip> tripsForTheDay)
        {
            var routes = new List<List<Trip>>();
            foreach (var tail in tails)
            {
                if (tail.Last().ToId == destination)
                {
                    routes.Add(tail);
                    continue;
                }

                routes.AddRange(ExpandList(tail, tripsForTheDay));
            }
            return routes.ToList();
        }

        private List<List<Trip>> ExpandList(IList<Trip> tail, IList<Trip> tripsForTheDay)
        {
            var routes = new List<List<Trip>>();
            var lastArrival = tail.Last().Date.Add(tail.Last().Arrival);
            var trips = tripsForTheDay
                .Where(x => x.FromId == tail.Last().ToId).ToList()
                .Where(x => !tail.Select(y => y.FromId).Contains(x.ToId))
                .Where(x => x.Date.Add(x.Departure) > lastArrival).ToList();
            foreach (var trip in trips)
            {
                var tailCopy = tail.Select(x => x).ToList();
                tailCopy.Add(trip);
                routes.Add(tailCopy);
            }
            return routes;
        }
    }
}