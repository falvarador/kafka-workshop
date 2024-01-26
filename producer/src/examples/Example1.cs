using Confluent.Kafka;

namespace Producer;

public class Example1
{
    public async Task Execute()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "127.0.0.1:9092",
            ClientId = "demo-producer",
            // SecurityProtocol = SecurityProtocol.SaslSsl,
            Acks = Acks.All,
            MessageTimeoutMs = 300000,
            BatchNumMessages = 10000,
            LingerMs = 5,
            CompressionType = CompressionType.Gzip,
        };

        var producer = new ProducerBuilder<string, string>(config).Build();

        var result = await producer.ProduceAsync("my_orders", new Message<string, string> { Key = "k1", Value = "An example from .NET producer" });

        Console.WriteLine($" Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");

        producer.Flush();
    }
}
