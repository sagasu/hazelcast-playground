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

            ListAllTopicsAndGroups();
            ListenToTopic();
        }

        private static void ListAllTopicsAndGroups()
        {
            var config = new AdminClientConfig();
            config.BootstrapServers = "localhost:9092";
            var admin = new AdminClientBuilder(config);

            using (var client = admin.Build())
            {
                var topics = client.GetMetadata(TimeSpan.FromMinutes(1)).Topics;
                Console.WriteLine("List of Topics");
                foreach (var topicMetadata in topics)
                {
                    Console.WriteLine(topicMetadata.Topic);
                }

                Console.WriteLine("List of Groups");
                var groups = client.ListGroups(TimeSpan.FromMinutes(1));
                foreach (var groupInfo in groups)
                {
                    Console.WriteLine(groupInfo.Group);
                }
                Console.ReadLine();
            }
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
