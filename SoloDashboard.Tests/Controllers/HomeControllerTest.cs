using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.Repository.Interfaces;
using Dashboard.Repository.Repos;

namespace SoloDashboard.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetTestData()
        {
            //Arrange
            IMockJobsRepository repo = new MockJobsRepository();

            //Act
            var result = repo.GetScheduleJobs();

            //Asert
            Assert.AreNotEqual(0, result.Count());

        }


    }
}
