using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tibox.Automation
{
    public class OrderPage
    {

        public static void LoginOrder()
        {
            LoginPage.Go();
            LoginPage.LoginAs("chino@gmail.com").WithPassword("123456").Login();
            Driver.Instance.Navigate().GoToUrl("http://localhost/Tibox.Web/#!/home");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Driver.Instance.FindElement(By.CssSelector("a[href='#!/order']")).Click();
        }
        public static OrderCommand CreateOrder(string orderNumber)
        {
            return new OrderCommand(orderNumber);
        }

        public static void DeleteOrder()
        {
            Driver.Instance.FindElement(By.Id("elim2")).Click();
            Driver.Instance.FindElement(By.CssSelector("button.btn.btn-danger.ng-binding.ng-scope")).Click();
        }

        public class OrderCommand
        {
            private string orderDate;
            private string orderNumber;
            private string customerId;
            private string totalAmount;

            public OrderCommand(string orderNumber)
            {
                this.orderNumber = orderNumber;
            }

            public OrderCommand withOrderDate(string orderDate)
            {
                this.orderDate = orderDate;
                return this;
            }

            public OrderCommand withCustomerId(string customerId)
            {
                this.customerId = customerId;
                return this;
            }

            public OrderCommand withTotalAmount(string totalAmount)
            {
                this.totalAmount = totalAmount;
                return this;
            }

            public void OrderCreate()
            {
                Driver.Instance.FindElement(By.CssSelector("button.btn.btn-primary")).Click();

                Driver.Instance.FindElement(By.Id("orderDate")).Clear();
                Driver.Instance.FindElement(By.Id("orderDate")).SendKeys(orderDate);

                Driver.Instance.FindElement(By.Id("orderNumber")).Clear();
                Driver.Instance.FindElement(By.Id("orderNumber")).SendKeys(orderNumber);

                Driver.Instance.FindElement(By.Id("customerId")).Clear();
                Driver.Instance.FindElement(By.Id("customerId")).SendKeys(customerId);

                Driver.Instance.FindElement(By.Id("totalAmount")).Clear();
                Driver.Instance.FindElement(By.Id("totalAmount")).SendKeys(totalAmount);

                Driver.Instance.FindElement(By.CssSelector("button.btn.btn-success.ng-binding.ng-scope")).Click();
            }

            public void OrderUpdate()
            {
                Driver.Instance.FindElement(By.Id("act2")).Click();

                Driver.Instance.FindElement(By.Id("orderDate")).Clear();
                Driver.Instance.FindElement(By.Id("orderDate")).SendKeys(orderDate);

                Driver.Instance.FindElement(By.Id("orderNumber")).Clear();
                Driver.Instance.FindElement(By.Id("orderNumber")).SendKeys(orderNumber);

                Driver.Instance.FindElement(By.Id("customerId")).Clear();
                Driver.Instance.FindElement(By.Id("customerId")).SendKeys(customerId);

                Driver.Instance.FindElement(By.Id("totalAmount")).Clear();
                Driver.Instance.FindElement(By.Id("totalAmount")).SendKeys(totalAmount);

                Driver.Instance.FindElement(By.CssSelector("button.btn.btn-success.ng-binding.ng-scope")).Click();
            }

            //public void OrderDelete()
            //{
            //    Driver.Instance.FindElement(By.Id("elim1")).Click();

            //    Driver.Instance.FindElement(By.Id("orderDate")).Clear();
            //    Driver.Instance.FindElement(By.Id("orderDate")).SendKeys(orderDate);

            //    Driver.Instance.FindElement(By.Id("orderNumber")).Clear();
            //    Driver.Instance.FindElement(By.Id("orderNumber")).SendKeys(orderNumber);

            //    Driver.Instance.FindElement(By.Id("customerId")).Clear();
            //    Driver.Instance.FindElement(By.Id("customerId")).SendKeys(customerId);

            //    Driver.Instance.FindElement(By.Id("totalAmount")).Clear();
            //    Driver.Instance.FindElement(By.Id("totalAmount")).SendKeys(totalAmount);

            //    Driver.Instance.FindElement(By.CssSelector("button.btn.btn-success.ng-binding.ng-scope")).Click();
            //}


        }
    }
}
