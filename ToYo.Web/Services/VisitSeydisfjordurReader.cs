using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ToYo.Web.Services
{
    public class VisitSeydisfjordurReader : IVisitSeydisfjordurReader
    {
        public List<List<string>> GetSchedule(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var tables = htmlDoc.DocumentNode.SelectNodes("//tbody");

            var trs = tables[0].ChildNodes[1].SelectNodes("//tr");

            var list = new List<List<string>>();
            foreach(HtmlNode tr in trs)
            {
                var whatisit = tr.ChildNodes.Where(x => x.Name == "td");
                list.Add(whatisit.Select(x => x.InnerText).ToList());
            }
            return list;
        }
    }
}