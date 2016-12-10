using System;
using System.Collections.Generic;
using System.Linq;
using ToYo.Web.Models;
using ToYo.Web.Extensions;
using System.Text.RegularExpressions;

namespace ToYo.Web.Services
{
    public class TripParser
    {
        public List<Trip> Parse(List<List<string>> schedule)
        {
            var daysDictionary = GetDaysDictionary(schedule);

            var referenceDate = DateTime.Today;

            var trips = new List<Trip>();

            for (var i = 1; i < schedule.Count; i++)
            {
                if(string.IsNullOrEmpty(schedule[i][0]))
                {
                    break;
                }
                for (var j = 1 ; j < schedule[i].Count() ; j++)
                {
                    if (string.IsNullOrEmpty(schedule[i][j]))
                    {
                        continue;
                    }
                    var result = Regex.Replace(schedule[i][j], @"\s+", "");
                    var time = result.Split(':');
                    var date = referenceDate.DayInWeek(daysDictionary[j]);
                    var departure = schedule[i][j];
                    var arrival = $"{Convert.ToInt32(time[0]) + 1:00}:{time[1]}";
                    trips.Add(new Trip
                    {
                        FromId = schedule[i][0] == "Frá Seyðisfirði" ? 10 : 9,
                        ToId = schedule[i][0] == "Frá Seyðisfirði" ? 9 : 10,
                        Date = date,
                        DepartureTime = departure,
                        ArrivalTime = arrival,
                        AgentId = 13
                    });
                }
            }

            return trips;
        }

        private Dictionary<int, DayOfWeek> GetDaysDictionary(List<List<string>> schedule)
        {
            var days = schedule[0].Select(x => ParseDay(x)).Where(x => x != null).ToList();
            var daysDic = new Dictionary<int, DayOfWeek>();
            for (var i = 0; i < days.Count(); i++)
            {
                daysDic.Add(i+1, days[i].Value);
            }
            return daysDic;
        }

        private DayOfWeek? ParseDay(string day)
        {
            switch (day.ToLower())
            {
                case "mán":
                    return DayOfWeek.Monday;
                case "þri":
                    return DayOfWeek.Tuesday;
                case "mið":
                    return DayOfWeek.Wednesday;
                case "fim":
                    return DayOfWeek.Thursday;
                case "fös":
                    return DayOfWeek.Friday;
                case "lau":
                    return DayOfWeek.Saturday;
                case "sun":
                    return DayOfWeek.Sunday;
                default:
                    return null;
            }
        }
    }
}