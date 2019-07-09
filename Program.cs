using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CircuitBreaker.Hystrix;

namespace HystrixTester
{
    class Program
    {
        static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hystrix.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddSingleton<ITestService, TestService>();

            serviceCollection.AddHystrixCommand<TestServiceCommand>("TestService", Configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine($"Iteration {i}: {serviceProvider.GetService<TestServiceCommand>().Execute()}");
                // Simulate some processing that takes time
                Thread.Sleep(500);
            }

            Console.WriteLine("Done");
        }
    }
}
