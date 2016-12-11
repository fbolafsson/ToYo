using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VisitSeydisfjordurModule.Extensions;

namespace VisitSeydisfjordurModule.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        [TestCase("Gildir tímabilið 10.06 – 31.08 2016", "2016-06-10", "2016-08-31")]
        [TestCase("Gildir tímabilið 1.september – 31.desember 2016", "2016-09-01", "2016-12-31")]
        public void ShouldParseTextToTwoDates(string text, DateTime expectedFrom, DateTime expectedTo)
        {
            // Arrange
            var datePart = text.Substring(17);
            var dateParts = datePart.Split(' ');
            var dayMonthFrom = dateParts[0].Split('.');
            var dayMonthTo = dateParts[2].Split('.');
            var year = dateParts[3];

            // Act
            var from = new DateTime(Convert.ToInt32(year), dayMonthFrom[1].ParseMonthToInt(), Convert.ToInt32(dayMonthFrom[0]));
            var to = new DateTime(Convert.ToInt32(year), dayMonthTo[1].ParseMonthToInt(), Convert.ToInt32(dayMonthTo[0]));

            /// Assert
            Assert.That(from, Is.EqualTo(expectedFrom));
            Assert.That(to, Is.EqualTo(expectedTo));
        }
    }
}
