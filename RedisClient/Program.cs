
using System;

namespace RedisClient
{
    class Program
    {
        static void Main()
        {
            var client = new SimpleRedisClient("localhost", 6379);
            client.DoJob();
            Console.ReadLine();
        }
    }
}
