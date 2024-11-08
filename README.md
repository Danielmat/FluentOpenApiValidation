# FluentOpenApiValidation
FluentOpenApiValidation is a .NET library for building  validation for OpenApi specification

---

### Get Started

FluentOpenApiValidation can be installed using the Nuget package manager or the `dotnet` CLI.

```
dotnet add package FluentOpenApiValidation
```
---

### Example
```csharp
using FluentOpenApiValidation;

 IContract contract =
      ContractBuilder
     .WithFileSource(FilePath.PATH)
        .WithPath("/pet/findByStatus")
            .WithGet(new OperationOptions
             {
                 Parameters = [new("status", PropertyType.String, In.Query)],
                 Security =
                             new("petstore_auth",
                                 new(["write:pets", "read:pets"]))
             })
               .WillRespond([new(HttpStatusCode.OK, MediaTypes.ApplicationJson, SchemaType.Array),
                   new(HttpStatusCode.OK, MediaTypes.ApplicationXml, SchemaType.Array),
                   new(HttpStatusCode.BadRequest)])
               .WillSecuritySchema(new("petstore_auth"))
               .WillSchema(new("Pet", [new("name", PropertyType.String)]))
       .Build();

 bool result = contract.Validate();

 contrat.Errors.ForEach(i => Console.WriteLine("{0}\t", i));
```

### License, Copyright etc

FluentOpenApiValidation has adopted the [Code of Conduct](https://github.com/Danielmat/FluentOpenApiValidation/blob/main/CODE_OF_CONDUCT.md) defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

FluentOpenApiValidation is copyright &copy; 2024 .NET Foundation, Daniel Manga and other contributors and is licensed under the [Apache2 license](https://github.com/Danielmat/FluentOpenApiValidation/blob/main/LICENSE/).
