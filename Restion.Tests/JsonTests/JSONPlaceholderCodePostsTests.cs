using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restion.Constants;
using Restion.Extensions;
using Restion.Tests.Models;

namespace Restion.Tests.JsonTests
{
    [TestClass]
    public class JsonPlaceHolderCodePostsTests
    {
        [TestMethod]
        [TestCategory("JsonPosts")]
        public async Task ShouldReturnAllPosts()
        {
            try
            {
                IRestionClient restionClient = new RestionClient().SetBaseAddress("http://jsonplaceholder.typicode.com");

                IRestionRequest restionRequest = new RestionRequest("/posts/");

                IRestionResponse<List<Post>> response = await restionClient.ExecuteRequestAsync<List<Post>>(restionRequest);

                if (response != null)
                {
                    if (response.Exception != null)
                    {
                        Assert.Fail(response.Exception.Message);
                    }
                    else
                    {
                       response.Content.Should().NotBeNull().And.HaveCount(100);
                    }
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("JsonPosts")]
        public async Task ShouldReturnOnePost()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                                                   .SetBaseAddress("http://jsonplaceholder.typicode.com");

                IRestionRequest restionRequest = new RestionRequest("/posts/1");

                IRestionResponse<Post> response = await restionClient.ExecuteRequestAsync<Post>(restionRequest);

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
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
        [TestCategory("JsonPosts")]
        public async Task ShouldPostAPost()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                                       .SetBaseAddress("http://jsonplaceholder.typicode.com");

                IRestionRequest restionRequest = new RestionRequest("/posts/")
                                         .WithHttpMethod(HttpMethod.Post)
                                         .WithContent(new Post()
                                         {
                                             Body = "Teste",
                                             Title = "Teste",
                                             UserId = 1
                                         })
                                         .WithContentMediaType(MediaTypes.ApplicationJson)
                                         .WithContentEnconding(Encoding.UTF8);

                IRestionResponse<Post> response = await restionClient.ExecuteRequestAsync<Post>(restionRequest);

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

        [TestMethod]
        [TestCategory("JsonPosts")]
        public async Task ShouldPutAPost()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                                       .SetBaseAddress("http://jsonplaceholder.typicode.com");

                IRestionRequest restionRequest = new RestionRequest("/posts/1")
                                         .WithHttpMethod(HttpMethod.Put)
                                         .WithContent(new Post()
                                         {
                                             Id = 1,
                                             Body = "TestePut",
                                             Title = "TestePut",
                                             UserId = 1
                                         })
                                         .WithContentMediaType(MediaTypes.ApplicationJson)
                                         .WithContentEnconding(Encoding.UTF8);

                IRestionResponse<Post> response = await restionClient.ExecuteRequestAsync<Post>(restionRequest);

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

                throw;
            }
        }

        [TestMethod]
        [TestCategory("JsonPosts")]
        public async Task ShouldDeleteAPost()
        {
            try
            {
                IRestionClient restionClient = new RestionClient()
                                       .SetBaseAddress("http://jsonplaceholder.typicode.com");

                IRestionRequest restionRequest = new RestionRequest("/posts/1")
                                        .WithHttpMethod(HttpMethod.Delete);

                IRestionResponse<Post> response = await restionClient.ExecuteRequestAsync<Post>(restionRequest);

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
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
        public void TesteDictionaryToQueryString()
        {
             var parameters = new Dictionary<string,string>()
             {
                 ["Id"] = "123",
                 ["Name"] = "Foo",
                 ["Version"] = "2.0"
             };

            var queryString = parameters.ToQueryString();

            Debug.WriteLine(queryString);
        }
 
    }
}
