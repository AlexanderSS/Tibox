using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Tibox.UnitOfWork;
using Tibox.WebApi.Controllers;
using Tibox.WebApi.Tests.MockData;
using System.Web.Http.Results;
using FluentAssertions;
using System.Linq;
using Tibox.Models;

namespace Tibox.WebApi.Tests
{
    public class OrderControllerTests
    {
        private readonly IUnitOfWork _unit;
        private OrderController _orderController;

        public OrderControllerTests()
        {
            _unit = new MockedUnitOfWork();
            _orderController = new OrderController(_unit);
        }

        [Fact]
        public void GetByIdBadRequest()
        {
            var result = _orderController.Get(-1) as BadRequestResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void GetByIdOkWithNull()
        {
            var result = _orderController.Get(400) as OkNegotiatedContentResult<Customer>;
            result.Content.Should().BeNull();
        }

        [Fact]
        public void GetByIdOk()
        {
            var orderToValidate = _unit.Orders.GetAll().ToList().FirstOrDefault();
            var result = _orderController.Get(orderToValidate.Id) as OkNegotiatedContentResult<Order>;
            result.Should().NotBeNull();
            result.Content.Id.Should().Be(orderToValidate.Id);
            result.Content.OrderDate.Should().Be(orderToValidate.OrderDate);
            result.Content.OrderNumber.Should().Be(orderToValidate.OrderNumber);
            result.Content.CustomerId.Should().Be(orderToValidate.CustomerId);
            result.Content.TotalAmount.Should().Be(orderToValidate.TotalAmount);
        }

        [Fact]
        public void PostCustomer()
        {
            _orderController.ModelState.Clear();
            _orderController.ModelState.AddModelError("fakeError", "FakeError");

            var result = _orderController.Post(null) as InvalidModelStateResult;
            result.Should().NotBeNull();
            result.ModelState.IsValid.Should().BeFalse();
            result.ModelState.Values.Count.Should().BeGreaterThan(0);
        }
    }
}
