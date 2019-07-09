using System;
using System.Threading;
using Steeltoe.CircuitBreaker.Hystrix;

namespace HystrixTester
{
    public class TestServiceCommand : HystrixCommand<string>
    {
        ITestService _testService;

        public TestServiceCommand(IHystrixCommandOptions options, ITestService testService) : base(options)
        {
            _testService = testService;
        }

        protected override string Run()
        {
            return _testService.GetString();
        }

        protected override string RunFallback()
        {
            return $"FALLBACK: {DateTime.MinValue.ToString()}";
        }
    }
}