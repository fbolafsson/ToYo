using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToYo.Web.Models.ViewModels
{
    public class TripRequestModel
    {
        public int From { get; set; }
        public int To { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

    }
}