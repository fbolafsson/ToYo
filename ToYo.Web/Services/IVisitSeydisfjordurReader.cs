using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToYo.Web.Services
{
    public interface IVisitSeydisfjordurReader
    {
        List<List<string>> GetSchedule(string html);
    }
}
