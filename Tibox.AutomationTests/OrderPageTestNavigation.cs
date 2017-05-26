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
    public class OrderPageTestNavigation
    {
        public OrderPageTestNavigation()
        {
            Driver.GetInstance(DriversOption.Chrome);
        }

        [Fact]
        public void OrderTest()
        {
            OrderPage.LoginOrder();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Driver.CloseInstance();
        }

        [Fact]
        public void OrderCreateRegister()
        {
            OrderPage.LoginOrder();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            OrderPage.CreateOrder("120334").withOrderDate("2017-05-25").withCustomerId("15").withTotalAmount("2500").OrderCreate();

            Thread.Sleep(TimeSpan.FromSeconds(4));
            Driver.CloseInstance();
        }

        [Fact]
        public void OrderUpdate()
        {
            OrderPage.LoginOrder();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            OrderPage.CreateOrder("12049").withOrderDate("2017-05-25").withCustomerId("1").withTotalAmount("100").OrderUpdate();

            Thread.Sleep(TimeSpan.FromSeconds(5));
            Driver.CloseInstance();
        }

        [Fact]
        public void OrderDelete()
        {
            OrderPage.LoginOrder();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            OrderPage.DeleteOrder();

            Thread.Sleep(TimeSpan.FromSeconds(5));
            Driver.CloseInstance();
        }
    }
}
