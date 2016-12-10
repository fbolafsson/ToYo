﻿using System;
using System.Collections.Generic;
using ToYo.Web.Models;
using System.Net.Http;

namespace ToYo.Web.Services
{
    public class VisitSeydisfjordurTripRepository : ITripRepository
    {
        private readonly IVisitSeydisfjordurReader visitSeydisfjordurReader;

        public VisitSeydisfjordurTripRepository(IVisitSeydisfjordurReader visitSeydisfjordurReader)
        {
            if (visitSeydisfjordurReader == null) throw new ArgumentNullException(nameof(visitSeydisfjordurReader));
            this.visitSeydisfjordurReader = visitSeydisfjordurReader;
        }

        // TODO: Það á eftir að klára að útfæra þetta, en þetta er dæmigerð síða sem þarf að scrape-a
        public IList<Trip> GetTrips(DateTime date)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.visitseydisfjordur.com/is/project")
            };

            var responseMessage = client.GetAsync("/ferdathjonusta-austurlands/").Result;
            var html = responseMessage.Content.ReadAsStringAsync().Result;

            var schedule = visitSeydisfjordurReader.GetSchedule(html);
            var tripParser = new TripParser();


            return tripParser.Parse(schedule);
        }
    }
}