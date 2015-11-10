# Restion
I like to call it an unifier of kingdoms portable class library for calling RESTful services in .NET. 
It unifies the fluent interface kingdon with HttpClient and make http request from PCL a lot more easier.
Written by [Vinicius Gualberto] (vinguan.brandedme.com) and licensed under [Apache 2](http://www.apache.org/licenses/LICENSE-2.0.html).

#Nuget
```
Install-Package Restion
```

# Features
* Built using Fluent Interface 
```csharp
IRestionClient restionClient = new RestionClient()
                                   .SetBaseAddress("http://foo.bar.com");

IRestionRequest restionRequest = new RestionRequest("/foo/")
                                     .WithHttpMethod(HttpMethod.Post)
                                     .WithContent(new Foo(){FooName = "1"})
                                     .WithContentMediaType(MediaTypes.ApplicationJson)
                                     .WithContentEnconding(Encoding.UTF8);
```
* Full Asynchronously
``` csharp
var response = await restionClient.ExecuteRequestAsync<IRestionRequest, 
                                                      Foo, 
                                                      RestionResponse<Foo>>(restionRequest);
```
* Full Extendable
```csharp
ISerializer serializer = new CustomSerializer(); 
IDeSerializer deSerializer = new CustomDeSerializer(); 

IRestionClient restionClient = new RestionClient()
                                   .SetBaseAddress("http://foo.bar.com")
                                   .SetSerializer(serializer)
                                   .SetDeserializer(deSerializer);

```
# Getting Started
1. Creating IRestionClient 
By default it uses [Json.NET](http://www.newtonsoft.com/json) for Serialization and Deserialization
```csharp
IRestionClient restionClient = new RestionClient()
                                   .SetBaseAddress("http://foo.bar.com");
```
2. Creating the IRestionRequest
```csharp
IRestionRequest restionRequest = new RestionRequest("/foo/")
                                     .WithHttpMethod(HttpMethod.Post)
                                     .WithContent(new Foo(){FooName = "1"})
                                     .WithContentMediaType(MediaTypes.ApplicationJson)
                                     .WithContentEnconding(Encoding.UTF8);
```
3. Executing the Request
It returns and instance of IRestionRequest<TContent>
```csharp
var response = await restionClient.ExecuteRequestAsync<IRestionRequest, 
                                                      Foo, 
                                                      RestionResponse<Foo>>(restionRequest);
```

# Contribute
Fork me and send the pull requests =).

# Doubts,Sugestions or other stuff
Contact me on twitter [@Vinguan](http://twitter.com/vinguan)
