using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restion.Tests.Models;

namespace Restion.Tests.LocalWebApiTests
{
    [TestClass]
    public class LocalWebApiTest
    {
        [TestMethod]
        [TestCategory("LocalWebApi")]
        public async Task ShouldReturnOneCustomer()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                    .SetBaseAddress("http://localhost/Restion.Tests.WebApi/api");

                var restionRequest = new RestionRequest("/Customer/?")
                                          .AddParameter("Name","123")
                                          .AddParameter("DateOfBirth",DateTime.Now.ToShortDateString());

                var response = await restionClient.ExecuteRequestAsync<IRestionRequest, Customer, RestionResponse<Customer>>(restionRequest);

                if (response != null)
                {
                    if (response.Exception != null)
                    {
                        Assert.Fail(response.Exception.Message);
                    }
                    else
                    {
                        response.Content.Should().NotBeNull();
                    }
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
