# Restion
I like to call it an unifier of kingdoms portable class library for calling RESTful services in .NET. 
It unifies the fluent interface kingdom with HttpClient and make http request from PCL a lot more easier.

##Compability
Compiled to Profile 344 of PCL which includes the plataforms .NET 4.5, Silverlight 5, Windows Phone 8.x, Windows 8.x. It can be used also on iOS and Android with [Xamarin](http://xamarin.com).

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
* Creating IRestionClient 
By default it uses [Json.NET](http://www.newtonsoft.com/json) for Serialization and Deserialization
```csharp
IRestionClient restionClient = new RestionClient()
                                   .SetBaseAddress("http://foo.bar.com");
```
* Creating the IRestionRequest
```csharp
IRestionRequest restionRequest = new RestionRequest("/foo/")
                                     .WithHttpMethod(HttpMethod.Post)
                                     .WithContent(new Foo(){FooName = "1"})
                                     .WithContentMediaType(MediaTypes.ApplicationJson)
                                     .WithContentEnconding(Encoding.UTF8);
```
* Executing the Request
It returns and instance of IRestionRequest<TContent>
```csharp
var response = await restionClient.ExecuteRequestAsync<IRestionRequest, 
                                                      Foo, 
                                                      RestionResponse<Foo>>(restionRequest);
```

# Author
Vinicius Gualberto [@Vinguan](http://twitter.com/vinguan).

# Contribute
Fork me and send the pull requests =).


