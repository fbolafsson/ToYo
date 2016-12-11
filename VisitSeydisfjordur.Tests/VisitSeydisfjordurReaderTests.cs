using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using VisitSeydisfjordurModule;

namespace VisitSeydisfjordur.Tests
{
    [TestFixture]
    public class VisitSeydisfjordurReaderTests
    {
        [Test]
        public void VisitSeydisfjordurShouldReturnSchedule()
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
            foreach(var schedule in result)
            {
                Console.WriteLine($"Gildir frá {schedule.EffectiveFrom:dd.MM.yyyy} til {schedule.EffectiveTo:dd.MM.yyyy}, Verð: {schedule.Price:c0}");
                foreach (var key in schedule.Dictionary.Keys)
                {
                    Console.Write($"{key,-5}\t");
                    foreach (var element in schedule.Dictionary[key])
                    {
                        if (!string.IsNullOrEmpty(element))
                        {
                            Console.Write($"{element,16}\t");
                        }
                        else
                        {
                            Console.Write($"{"N/A",16}\t");
                        }
                    }
                    Console.WriteLine();
                }
            }
            
            Assert.That(result, Is.Not.Null);
        }
    }
}
