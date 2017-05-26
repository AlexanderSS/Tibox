using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibox.Models;
using Tibox.Repository.Northwind;

namespace Tibox.WebApi.Tests.MockData
{
    public class MockedOrderRepository : IOrderRepository
    {
        private List<Order> orders;

        public MockedOrderRepository(){
            var fixture = new Fixture();
            orders = fixture.CreateMany<Order>(30).ToList();
        }

        public bool Delete(Order entity)
        {
            return orders.Remove(entity);
        }

        public IEnumerable<Order> GetAll()
        {
            return orders;
        }

        public Order GetEntityById(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public int Insert(Order entity)
        {
            orders.Add(entity);
            return entity.Id;
        }

        public Order OrderByOrderNumber(string orderNumber)
        {
            return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public Order OrderWithOrderItems(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public bool SaveOrderAndOrderItems(Order order, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order entity)
        {
            var updateEntity = orders.FirstOrDefault(o => o.Id == entity.Id);
            if (updateEntity == null) return false;
            updateEntity = entity;
            return true;
        }
    }
}
