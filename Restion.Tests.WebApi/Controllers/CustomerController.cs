using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restion.Tests.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Customer/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        public Customer Get([FromUri] Customer customer)
        {
            return new Customer()
            {
                DateOfBirth = new DateTime(1985,7,22),
                Name = "John"
            };
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }

    public class Customer
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
