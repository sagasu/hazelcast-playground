using System;
using System.Threading;
using Confluent.Kafka;

using Confluent.Kafka.Admin;



namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = new AdminClientConfig();
            config.BootstrapServers = "localhost:9092";
            var admin = new AdminClientBuilder(config);
            var client = admin.Build();
            var groups = client.ListGroups(TimeSpan.FromMinutes(1));

            //ListenToTopic();
        }

        private static void ListenToTopic()
        {
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
