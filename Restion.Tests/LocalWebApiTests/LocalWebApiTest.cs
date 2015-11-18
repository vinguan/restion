using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;
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

                var response = await restionClient.ExecuteRequestAsync<Customer>(restionRequest);

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
                else
                {
                    Assert.Fail();
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("LocalWebApi")]
        public async Task ShouldReturnTrueForTestHeader()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                    .SetBaseAddress("http://localhost/Restion.Tests.WebApi/api");

                var restionRequest = new RestionRequest("/Test/").AddHeader("Test-Header", "Test-Header-Value");

                var response = await restionClient.ExecuteRequestAsync<BolleanResponse>(restionRequest);

                if (response != null)
                {
                    if (response.Exception != null)
                    {
                        Assert.Fail(response.Exception.Message);
                    }
                    else
                    {
                        response.Content.Should().Be(new BolleanResponse(){Response = true});
                    }
                }
                else
                {
                    Assert.Fail();
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
