using System;
using System.Linq;
using Tibox.Automation;
using Xunit;

namespace Tibox.AutomationTests
{
    public class BasicTest
    {
        [Fact]
        public void GoToGoogle()
        {
            var test = new SimpleTest();
            test.Navigate();
        }
    }
}
