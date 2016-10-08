using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ToYo.Web.Models.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<SelectListItem> Places { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    }
}
