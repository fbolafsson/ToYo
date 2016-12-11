using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitSeydisfjordurModule
{
    public interface IVisitSeydisfjordurReader
    {
        List<Schedule> GetSchedule(string html);
    }
}
