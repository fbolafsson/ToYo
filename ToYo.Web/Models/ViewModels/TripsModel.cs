using System.Collections.Generic;
using System.Web.Mvc;

namespace ToYo.Web.Models.ViewModels
{
    public class TripsModel
    {
        public IEnumerable<SelectListItem> Places { get; set; }
    }
}