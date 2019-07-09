using System;

namespace HystrixTester
{
    public class TestService : ITestService
    {
        private static Random _rnd = new Random();

        public string GetString()
        {

            // Simulate a probable error
            if (_rnd.Next() % 2 == 0)
            {
                Console.Write("Error will happen -> ");
                throw new Exception("Some exception");
            }

            return $"Res: {DateTime.Now.ToString()}";
        }
    }
}