using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tibox.Automation;
using Xunit;

namespace Tibox.AutomationTests
{
    public class LoginPageTestNavigation
    {
        public LoginPageTestNavigation()
        {
            Driver.GetInstance(DriversOption.Chrome);
        }

        [Fact]
        public void LoginTest()
        {
            LoginPage.Go();
            LoginPage.LoginAs("juvega@gmail.com").WithPassword("12345678").Login();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            LoginPage.GetUrl().Should().Be("http://localhost/Tibox.Angular/#!/product");
            LoginPage.Logout();
            Driver.CloseInstance();
        }

        [Fact]
        public void LoginWrongTest()
        {
            LoginPage.Go();
            LoginPage.LoginAs("juvega@gmail.com").WithPassword("87654321").Login();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            LoginPage.IsAlertErrorPresent().Should().BeTrue();
            Driver.CloseInstance();
        }
    }
}
