using Confluent.Kafka;
using System;
using System.Net;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092,localhost:9092",
                ClientId = Dns.GetHostName(),
                
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var t = producer.ProduceAsync("topic", new Message<Null, string> { Value = "hello world" });
                t.ContinueWith(task => {
                    if (task.IsFaulted)
                    {
                        
                    }
                    else
                    {
                        

                        Console.WriteLine($"Wrote to offset: {task.Result.Offset}");
                    }
                });
            }
        }
    }
}
