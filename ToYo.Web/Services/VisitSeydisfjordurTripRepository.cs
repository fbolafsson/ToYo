using System;
using System.Collections.Generic;
using ToYo.Web.Models;
using System.Net.Http;
using HtmlAgilityPack;

namespace ToYo.Web.Services
{
    public class VisitSeydisfjordurTripRepository : ITripRepository
    {
        // TODO: Það á eftir að klára að útfæra þetta, en þetta er dæmigerð síða sem þarf að scrape-a
        public IList<Trip> GetTrips(DateTime date)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.visitseydisfjordur.com/is/project")
            };

            var responseMessage = client.GetAsync("/ferdathjonusta-austurlands/").Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(result);

            foreach (HtmlNode table in htmlDoc.DocumentNode.SelectNodes("//table"))
            {
                var text = table.ChildNodes[1].SelectNodes("//tr");
            }

            return new List<Trip>();
        }
    }
}