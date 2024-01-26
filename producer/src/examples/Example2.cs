using System.Text.Json;
using Confluent.Kafka;

namespace Producer;

public class Bookmark
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class Example2
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

        var jsonSerializer = new CustomJsonSerializer<Bookmark>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultBufferSize = 1024,
        });

        var bookmark = new Bookmark
        {
            Title = "Kafka Producer .NET from JSON",
            Description = "This is an example from Teams Call with sir Alex and Adriel.",
        };

        var producer = new ProducerBuilder<string, Bookmark>(config)
            .SetValueSerializer(jsonSerializer)
            .Build();

        var result = await producer.ProduceAsync("my_orders", new Message<string, Bookmark> { Key = "k1", Value = bookmark });

        Console.WriteLine($" Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");

        // Flush is an asynchronous method that dumps the message into a Kafka topic.
        // If you dump each message individually you are creating a synchronous producer.
        // So it is better to create a batch of messages and then send them to Kafka.
        // For this example, we have created a synchronous producer.
        producer.Flush();
    }
}
