using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ToYo.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        [TestCase("2016-12-10", DayOfWeek.Monday, "2016-12-05")]
        [TestCase("2016-12-11", DayOfWeek.Monday, "2016-12-05")]
        [TestCase("2016-12-07", DayOfWeek.Monday, "2016-12-05")]
        [TestCase("2016-12-05", DayOfWeek.Monday, "2016-12-05")]
        [TestCase("2016-12-05", DayOfWeek.Tuesday, "2016-12-06")]
        [TestCase("2016-12-05", DayOfWeek.Friday, "2016-12-09")]
        [TestCase("2016-12-08", DayOfWeek.Friday, "2016-12-09")]
        [TestCase("2016-12-08", DayOfWeek.Sunday, "2016-12-11")]
        public void ShouldCalculateDaysCorrectly(DateTime referenceDate, DayOfWeek dayOfWeek, DateTime expectedDate)
        {
            var mondayDateDiff = ToInt(dayOfWeek) - ToInt(referenceDate.DayOfWeek);
            var result = referenceDate.AddDays(mondayDateDiff);

            Assert.That(result, Is.EqualTo(expectedDate));
        }

        private int ToInt(DayOfWeek day)
        {
            if(day == DayOfWeek.Sunday)
            {
                return 7;
            }
            return (int)day;
        }
    }
}
