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
        private TripRepository tripRepository;
        private PlaceRepository placeRpository;
        private AgentRepository agentRpository;

        public HomeController()
        {
            tripRepository = new TripRepository();
            placeRpository = new PlaceRepository();
            agentRpository = new AgentRepository();
        }

        public ActionResult Index()
        {
            var repo = new PlaceRepository();
            var model = new HomeModel {
                Places = repo.GetPlaces()
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                Date = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        public ActionResult Trip(TripRequestModel model)
        {
            var tripRepository = new TripRepository();
            var trips = tripRepository.GetTrips();
            var tails = trips.Where(x => x.FromId == model.From).Where(x => x.Date.Date == model.Date.Value.Date).Select(x => new List<Trip> { x }).ToList();

            var possibleRouts = new List<List<Trip>>();

            for(var i = 0; i < 7; i++)
            {
                tails = DoTheThing(tails, model.To, model.Date.Value);
            }

            var routes = tails.Where(x => x.Last().ToId == model.To && x.Count() < 7).ToList().OrderBy(x => x.Count);
            
            var tripModel = routes.Select(x => 
                new TripsModel
                {
                    Trips = x.Select(y =>
                    new TripModel()
                    {
                        From = placeRpository.Get(y.FromId),
                        To = placeRpository.Get(y.ToId),
                        Agent = agentRpository.GetAgent(y.AgentId),
                        Departure = y.Date.Add(y.Departure),
                        Arrival = y.Date.Add(y.Arrival)
                    }).ToList(),
                    JourneyTime = new DateTime().Add(x.Last().Arrival.Subtract(x.First().Departure)),
                    TotalPrice = x.Sum(y => y.Price)
                }).OrderBy(x => x.JourneyTime).ToList();
            return View(tripModel);
        }

        private List<List<Trip>> DoTheThing(List<List<Trip>> tails, int destination, DateTime date)
        {
            var routes = new List<List<Trip>>();
            foreach (var tail in tails)
            {
                if (tail.Last().ToId == destination)
                {
                    routes.Add(tail);
                    continue;
                }

                routes.AddRange(ExpandList(tail, date));
            }
            return routes.ToList();
        }

        private List<List<Trip>> ExpandList(IList<Trip> tail, DateTime date)
        {
            var routes = new List<List<Trip>>();
            var lastArrival = tail.Last().Date.Add(tail.Last().Arrival);
            var trips = tripRepository.GetTrips()
                .Where(x => x.FromId == tail.Last().ToId).ToList()
                .Where(x => !tail.Select(y => y.FromId).Contains(x.ToId))
                .Where(x => lastArrival < x.Date.Add(x.Departure))
                .Where(x => x.Date == date).ToList();
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