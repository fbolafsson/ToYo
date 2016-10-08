using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToYo.Web.Models
{
    public class Place
    {
        public Place(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}