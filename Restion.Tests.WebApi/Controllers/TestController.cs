using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restion.Tests.WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public BolleanResponse TestHeader()
        {
            IEnumerable<string> headers;

            if (Request.Headers.TryGetValues("Test-Header", out headers))
            {
                var testHeader = headers.First();

                if (testHeader != null && testHeader == "Test-Header-Value")
                {
                    return new BolleanResponse() { Response = true };
                }
            }

            return new BolleanResponse() { Response = false };
        }

        public class BolleanResponse
        {
            public bool Response { get; set; }
        }
    }
}
