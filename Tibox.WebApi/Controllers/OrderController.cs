using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tibox.Models;
using Tibox.UnitOfWork;

namespace Tibox.WebApi.Controllers
{
    [RoutePrefix("order")]
    [Authorize]
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Orders.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Orders.Insert(order);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Orders.Update(order);
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _unit.Orders.Delete(new Order { Id = id });
            return Ok(new { delete = true });
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Orders.GetAll());
        }
    }
}
