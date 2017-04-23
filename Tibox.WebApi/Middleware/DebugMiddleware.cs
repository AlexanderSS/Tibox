using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Tibox.WebApi.Middleware
{
    public class DebugMiddleware
    {
        private Func<IDictionary<string, object>, Task> _next;

        public DebugMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            Debug.WriteLine($"Consulta entrante:{context.Request.Path }");
            await _next(environment);
            Debug.WriteLine($"Consulta saliente:{context.Request.Path }");
        }
    }
}