using System;
using System.Linq;
using Xunit;
using WebApi;

namespace TestWebApi
{
    public class LeapYearControllerFixture
    {
        [Fact]
        public void TestDetails()
        {
            LeapYearController leapYearController = new LeapYearController();
            var result = leapYearController.Details(2000);
            
        }
    }
}
