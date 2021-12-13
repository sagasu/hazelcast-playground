using System;
using System.Threading.Tasks;
using Hazelcast;
using Hazelcast.Core;

namespace HazelcastPlayground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SetData();
            await GetData();;
        }

        private static async Task GetData()
        {
            var options = new HazelcastOptionsBuilder().Build();
            options.ClusterName = "hello-world";

            await using var client = await HazelcastClientFactory.StartNewClientAsync(options);


            var map = await client.GetMapAsync<string, string>("my-distributed-map");
            foreach (var entry in await map.GetEntriesAsync())
                Console.WriteLine($"{entry.Key}: {entry.Value}");

            //client.Shutdown();
        }

        private static async Task SetData()
        {
            var options = new HazelcastOptionsBuilder().Build();
            options.ClusterName = "hello-world";

            var client = await HazelcastClientFactory.StartNewClientAsync(options);

            var map = await client.GetMapAsync<string, string>("my-distributed-map");

            await map.PutAsync("1", "John");
            await map.PutAsync("2", "Mary");
            await map.PutAsync("3", "Jane");
        }
    }
}
