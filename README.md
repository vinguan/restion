# Restion
[![Join the chat at https://gitter.im/vinguan/Restion](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/vinguan/Restion?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

<img src="https://raw.githubusercontent.com/vinguan/restion/master/restion-logo.png" width="200">

I like to call it an unifier of kingdoms portable class library for calling RESTful services in .NET. 
It unifies the fluent interface kingdom with HttpClient and make http requests a lot more easier.

# Compability
.Net Standard 1.1 or superior

# Nuget
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
IRestionResponse<Foo> response = await restionClient.ExecuteRequestAsync<Foo>(restionRequest);
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
IRestionResponse<Foo> response = await restionClient.ExecuteRequestAsync<Foo>(restionRequest);
```

# Author
Vinicius Gualberto [@Vinguan](http://twitter.com/vinguan).

# Contribute
Fork me and send the pull requests =).


