using System;
using System.Collections.Generic;
using System.Linq;
using VisitSeydisfjordurModule.Extensions;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace VisitSeydisfjordurModule
{
    public class VisitSeydisfjordurReader : IVisitSeydisfjordurReader
    {
        public List<Schedule> GetSchedule(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var tables = htmlDoc.DocumentNode.SelectNodes("//table");

            var schedules = new List<Schedule>();
            foreach(var table in tables)
            {
                var parentNode = table.ParentNode;

                var infotext = parentNode.SelectNodes(".//em")[0].InnerText;
                var effectiveFrom = GetFromDateFromText(infotext);
                var effectiveTo = GetToDateFromText(infotext);

                var regex = new Regex(@"Fullorðnir: [0-9]*\.[0-9]* kr");
                var match = regex.Match(html);
                var price = Convert.ToDecimal(Regex.Replace(match.Value, @"Fullorðnir: |\.| kr", ""));

                var schedule = new Schedule
                {
                    EffectiveFrom = effectiveFrom,
                    EffectiveTo = effectiveTo,
                    Price = price
                };

                var trs = table.ChildNodes[1].SelectNodes(".//tr");

                var list = new List<List<string>>();
                foreach (HtmlNode tr in trs)
                {
                    var childNodes = tr.ChildNodes.Select(x => x.InnerText).ToList();
                    list.Add(tr.ChildNodes.Where(x => x.Name == "td").Select(x => x.InnerText).ToList());
                }

                var dictionary = new Dictionary<string, List<string>>();
                for (var i = 1; i < list.Count; i++)
                {
                    for (var j = 0; j < list[i].Count; j++)
                    {
                        var header = list[0][j];
                        var key = string.IsNullOrEmpty(header) ? "Dest" : header;
                        var value = dictionary.ContainsKey(key) ? dictionary[key] : new List<string>();
                        var index = key == "Dest" ? list[i][j] : CleanString(list[i][j]);
                        value.Add(string.IsNullOrEmpty(list[i][j]) ? null : index);
                        dictionary.Remove(key);
                        dictionary.Add(key, value);
                    }
                }
                schedule.Dictionary = dictionary;
                schedules.Add(schedule);
            }
            return schedules;
        }

        private string CleanString(string dirtyString)
        {
            return Regex.Replace(dirtyString, @"\s+|\*", "");
        }

        public DateTime GetFromDateFromText(string text)
        {
            //"Gildir tímabilið dd.mmmm – dd.mmmm yyyy"
            var datePart = text.Substring(17);
            var dateParts = datePart.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var dayMonthFrom = dateParts[0].Split('.');
            return new DateTime(Convert.ToInt32(dateParts[3]), dayMonthFrom[1].ParseMonthToInt(), Convert.ToInt32(dayMonthFrom[0]));
        }

        public DateTime GetToDateFromText(string text)
        {
            //"Gildir tímabilið dd.mmmm – dd.mmmm yyyy"
            var datePart = text.Substring(17);
            var dateParts = datePart.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var dayMonthTo = dateParts[2].Split('.');
            return new DateTime(Convert.ToInt32(dateParts[3]), dayMonthTo[1].ParseMonthToInt(), Convert.ToInt32(dayMonthTo[0]));
        }
    }
}
