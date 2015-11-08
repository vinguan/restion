using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restion.Deserialization;
using Restion.Serialization;
using Restion.Tests.Models;

namespace Restion.Tests.XmlTests
{
    //[TestClass]
    public class XmlTests
    {
        //[TestIgnore]
        //[TestCategory("XmlPosts")]
        //public async Task ShouldReturnAllPosts()
        //{
        //    try
        //    {
        //        IRestionClient restionClient = new RestionClient(new XmlSerializer(), 
        //                                                         new XmlDeserializer())
        //                                       .SetBaseAddress("http://localhost:2695/api");

        //        var restionRequest = new RestionRequest("/post/");

        //        var response = await restionClient.ExecuteRequestAsync<RestionRequest, List<Post>, RestionResponse<List<Post>>>(restionRequest);

        //        if (response != null && response.Exception != null)
        //        {
        //            Assert.Fail(response.Exception.Message);
        //        }
        //        else
        //        {
        //            response.Should().NotBeNull();

        //            if (response != null)
        //                response.Content.Should().NotBeNull().And.HaveCount(3);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //}

        public void Teste()
        {
            
        }
    }
}
