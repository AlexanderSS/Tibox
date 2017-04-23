using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tibox.Models;

namespace Tibox.WebApi.Controllers
{
    [RoutePrefix("orderitem")]
    [Authorize]
    public class OrderItemController : BaseController
    {
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.OrderItems.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.OrderItems.Insert(orderItem);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.OrderItems.Update(orderItem);
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _unit.OrderItems.Delete(new OrderItem { Id = id });
            return Ok(new { delete = true });
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.OrderItems.GetAll());
        }
    }
}
