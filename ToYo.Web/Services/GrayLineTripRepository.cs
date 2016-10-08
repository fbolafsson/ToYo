using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ToYo.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace ToYo.Web.Services
{
    public class GrayLineTripRepository : ITripRepository
    {
        public IList<Trip> GetTrips(DateTime date)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.grayline.is")
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer nOqBJw1JJFYjfiHmvzUp8_N4ShI3cxxfi3p30zu_Ow2vRoPjy3ro2eHgk3Dwnk0oXojL5p9861_jasH-pP4sOhydkGJox2G5j0mBW-NDiIRUexP2F-tRttAjopGJ2QV5aay_xdtx4VwrX_1KDKLhop32-tpy7i-py4me7BGmuimEOxNbScYo3epjYUWhJr_dAQtT9zqBlsrS7tNbnGwcuVcL-shwdS3MJrav-uqi5tHMSlMFUuqSswWLy8ZAyZtdqbxkioGDfUQXi_S-am35siZV9I37pfBgSG4zEKYye90BV2SuhQJMRjC43IQnrt1ywi-vtXO5ezvIGsrsBlTJ_3cOVjzmWdloY6uu_qF_MLvzpnHyiIaBVn3p7dr-_3L5x6jBHYStpgWEZy_Xocm3KjitRQXq7dc0nvKRSajHK220jtEVvy2DiNVzHhcIGmkZeJqJkP3-0JBqIjF8Cf4dYVTAs11OKv1nIqz4hPHNRdHdD2oDZwdhCLBLJyfCEt5jfm_OakodujHpWG-kHnLbeA");
            var json = JsonConvert.SerializeObject(new
            {
                Tours = new[] { new {
                    TourNumber = "AH200",
                    FromDeparture = date,
                    UntilDeparture = date.AddDays(1).AddHours(-1),
                    Passengers = new [] { new {
                        AgeGroupDescription = "Adults",
                        NumberOfPAX = "2"
                    } }
                }, new {
                    TourNumber = "AH201",
                    FromDeparture = date,
                    UntilDeparture = date.AddDays(1).AddHours(-1),
                    Passengers = new [] { new {
                        AgeGroupDescription = "Adults",
                        NumberOfPAX = "2"
                    } }
                }},
                CurrencyCode = "ISK",
                AgentProfileId = 30
            });
            var somResponse = client.PostAsync("/api/tours",
                new StringContent(json, Encoding.UTF8, "application/json")).Result;
            var graylineResult = somResponse.Content.ReadAsAsync<GraylineResult>().Result;

            return graylineResult.TourDepartures.Select(x => new Trip {
                FromId = x.TourNumber == "AH201" ? 1 : 2,
                ToId = x.TourNumber == "AH201" ? 2 : 1,
                DepartureTime = x.DepartureText.Substring(0, 5),
                ArrivalTime = new DateTime().Add(ToTimeSpan(x.DepartureText)).AddMinutes(x.DurationMinutes).ToString("HH:mm"),
                Date = x.Departure.Date,
                AgentId = 12,
                Price = x.Prices.FirstOrDefault()?.PricePerPAX ?? null
            }).ToList();
        }

        private TimeSpan ToTimeSpan(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                return new TimeSpan();
            }
            var timespan = time.Split(':');
            return new TimeSpan(Convert.ToInt32(timespan[0]), Convert.ToInt32(timespan[1].Substring(0,2)), 0);
        }

        private class GraylineResult
        {
            public IEnumerable<GraylineTour> TourDepartures { get;set;}
        }

        private class GraylineTour
        {
            public string TourNumber { get; set; }
            public DateTime Departure { get; set; }
            public string DepartureText { get; set; }
            public int DurationMinutes { get; set; }
            public bool Available { get; set; }
            public IEnumerable<Prices> Prices { get; set; }
        }

        private class Prices
        {
            public decimal PricePerPAX { get; set; }

        }
    }
}