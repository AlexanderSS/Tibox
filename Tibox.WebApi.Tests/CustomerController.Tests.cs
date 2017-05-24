using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Tibox.UnitOfWork;
using Tibox.WebApi.Controllers;
using Tibox.WebApi.Tests.MockData;
using System.Web.Http.Results;
using FluentAssertions;
using Tibox.Models;
using System.Linq;

namespace Tibox.WebApi.Tests
{
    public class CustomerControllerTests
    {
        private readonly IUnitOfWork _unit;
        private CustomerController _customerController;

        public CustomerControllerTests()
        {
            _unit = new MockedUnitOfWork();
            _customerController = new CustomerController(_unit);
        }

        [Fact]
        public void GetByIdBadRequest()
        {
            var result = _customerController.Get(-1) as BadRequestResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void GetByIdOkWithNull()
        {
            var result = _customerController.Get(99999999) as OkNegotiatedContentResult<Customer>;
            result.Content.Should().BeNull();
        }

        [Fact]
        public void GetByIdOk()
        {
            var customerToValidate = _unit.Customers.GetAll().ToList().FirstOrDefault();
            var result = _customerController.Get(customerToValidate.Id) as OkNegotiatedContentResult<Customer>;
            result.Should().NotBeNull();
            result.Content.Id.Should().Be(customerToValidate.Id);
            result.Content.City.Should().Be(customerToValidate.City);
            result.Content.Country.Should().Be(customerToValidate.Country);
            result.Content.FirstName.Should().Be(customerToValidate.FirstName);
            result.Content.LastName.Should().Be(customerToValidate.LastName);
            result.Content.Phone.Should().Be(customerToValidate.Phone);
        }

        [Fact]
        public void PostCustomer()
        {
            _customerController.ModelState.Clear();
            _customerController.ModelState.AddModelError("fakeError", "FakeError");

            var result = _customerController.Post(null) as InvalidModelStateResult;
            result.Should().NotBeNull();
            result.ModelState.IsValid.Should().BeFalse();
            result.ModelState.Values.Count.Should().BeGreaterThan(0);
        }
    }
}
