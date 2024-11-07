using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using FluentOpenApiValidation;
using Perfolizer.Horology;
using System.Net;
using Xunit.Abstractions;

namespace FluentOpenApiValidationTest
{
    public class TestCases
    {
        private readonly ITestOutputHelper output;

        public TestCases(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetPetsByStatus_WithValidParameters_ShouldReturnMultipleCodes()
        {
            IContract contrat =
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

            bool result = contrat.Validate();

            contrat.Errors.ForEach(i => output.WriteLine("{0}\t", i));

            Assert.True(result);
        }

        [Fact]
        public void CreateStoreOrder_WithValidRequest_ShouldReturnSuccess()
        {
            IContract contrat =
                 ContractBuilder
                .WithFile(() => FilePath.LoadFile())
                .WithPath("/store/order")
                  .WithPost(new PostOperationOptions
                  {
                      Request = new(MediaTypes.ApplicationJson)
                  })
                      .WillRespond([new Response(HttpStatusCode.OK)])
                      .WillSchema(new Schema("Order", [new("quantity", PropertyType.Integer)]))
                  .Build();

            bool result = contrat.Validate();

            contrat.Errors.ForEach(i => output.WriteLine("{0}\t", i));

            Assert.True(result);
        }

        [Fact]
        public void DeletePet_WithValidApiKey_ShouldReturnSuccess()
        {
            IContract contrat =
                 ContractBuilder
                .WithFile(() => FilePath.LoadFile())
                .WithPath("/pet")
                  .WithPut(new PostOperationOptions
                  {
                      Request = new(MediaTypes.ApplicationJson),
                      Security = new("petstore_auth",
                                    new(["write:pets", "read:pets"]))
                  })
                      .WillRespond([new Response(HttpStatusCode.OK)])
                      .WillSecuritySchema(new("petstore_auth"))
                      .WillSchema(new Schema("Pet", [new("name", PropertyType.String)]))
                  .Build();

            bool result = contrat.Validate();

            contrat.Errors.ForEach(i => output.WriteLine("{0}\t", i));

            Assert.True(result);
        }

        [Fact]
        public void DeletePet_WithValidApiKey_ShouldReturnBadRequest()
        {
            IContract contrat =
                 ContractBuilder
                .WithFile(() => FilePath.LoadFile())
                .WithPath("/pet/{petId}")
                  .WithDelete(
                     new OperationOptions
                     {
                         Parameters = [new("api_key", PropertyType.String, In.Header)],
                         Security =
                                new("petstore_auth",
                                    new(["write:pets", "read:pets"]))
                     })
                      .WillRespond([new Response(HttpStatusCode.BadRequest)])
                      .WillSecuritySchema(new("petstore_auth"))
                      .WillSchema()
                  .Build();

            bool result = contrat.Validate();

            contrat.Errors.ForEach(i => output.WriteLine("{0}\t", i));

            Assert.True(result);
        }

        //[Fact]
        public void RunPerformanceBenchmarks()
        {
            var logger = new AccumulationLogger();

            var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddLogger(logger).WithSummaryStyle(SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond))
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            BenchmarkRunner.Run<OpenApiBenchmarks>(config);

            output.WriteLine(logger.GetLog());
        }
    }

    [RankColumn]
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80, baseline: true)]
    public class OpenApiBenchmarks
    {
        [Benchmark]
        public void ProcessGet()
        {
            IContract contrat =
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

            contrat.Validate();
        }

        [Benchmark]
        public void ProcessPost()
        {
            IContract contrat =
                 ContractBuilder
                .WithFile(() => FilePath.LoadFile())
                .WithPath("/store/order")
                  .WithPost(new PostOperationOptions
                  {
                      Request = new(MediaTypes.ApplicationJson)
                  })
                      .WillRespond([new Response(HttpStatusCode.OK)])
                      .WillSchema(new Schema("Order", [new("quantity", PropertyType.Integer)]))
                  .Build();

            contrat.Validate();
        }
    }
}