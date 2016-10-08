using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToYo.Web.Models;

namespace ToYo.Web.Services
{
    public interface ITripRepository
    {
        IList<Trip> GetTrips(DateTime date);
    }
}
