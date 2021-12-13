using System;
using Hazelcast.Core;

namespace HazelcastPlayground3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the client and connect to the cluster
            var hz = HazelcastClient.NewHazelcastClient();
            // Create a Distributed Map in the cluster
            var map = hz.GetMap("my-distributed-map");
            //Standard Put and Get
            map.put("1", "John");
            map.put("2", "Mary");
            map.put("3", "Jane");
            // Shutdown the client
            hz.Shutdown();
        }
    }
}
