using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using System.Text.RegularExpressions;

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

        [Test]
        public void ShouldBe()
        {
            //Arrange
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.visitseydisfjordur.com/is/project")
            };

            var responseMessage = client.GetAsync("/ferdathjonusta-austurlands/").Result;
            var httpResult = responseMessage.Content.ReadAsStringAsync().Result;


            var regex = new Regex(@"Fullorðnir: [0-9]*\.[0-9]* kr");
            var match = regex.Match(httpResult);

            Assert.True(match.Success);
            if (match.Success)
            {
                Console.WriteLine("Found Match for {0}", match.Value);
                Console.WriteLine("ID was {0}", match.Groups[1].Value);
            }

        }

    }
}
