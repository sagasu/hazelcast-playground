using System;
using System.Threading;
using Confluent.Kafka;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var topics = "topic";
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topics);

                while (true)
                {
                    var consumeResult = consumer.Consume(CancellationToken.None);
                    Console.WriteLine(consumeResult.Message.Value);
                    
                    // handle consumed message.

                }

                consumer.Close();
            }
        }
    }
}
