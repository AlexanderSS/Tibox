using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tibox.Models;

namespace Tibox.WebApi.Controllers
{
    [RoutePrefix("product")]
    [Authorize]
    public class ProductController : BaseController
    {
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Products.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Products.Insert(product);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Products.Update(product);
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _unit.Products.Delete(new Product { Id = id });
            return Ok(new { delete = true });
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Products.GetAll());
        }
    }
}
