using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using NUnit.Framework;
using ToYo.Web.Services;
using HtmlAgilityPack;

namespace ToYo.Tests
{
    [TestFixture]
    public class SeydisfjordurReaderTests
    {
        [Test]
        public void ShouldDoSomething()
        {
            //Arrange
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.visitseydisfjordur.com/is/project")
            };

            var responseMessage = client.GetAsync("/ferdathjonusta-austurlands/").Result;
            var httpResult = responseMessage.Content.ReadAsStringAsync().Result;

            var sut = new VisitSeydisfjordurReader();

            //Act
            var result = sut.GetSchedule(httpResult);

            //Assert
            foreach(var node in result)
            {
                foreach(var element in node)
                {
                    if(!string.IsNullOrEmpty(element))
                    {
                        Console.Write($"{element}\t");
                    }
                    else
                    {
                        Console.Write($"N/A\t");
                    }
                }
                Console.WriteLine();
            }
            Assert.That(result, Is.Not.Null);
        }
    }
}
