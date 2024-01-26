using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Producer;

public class KafkaSerializer
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class Example3
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

        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            // Note: you can specify more than one schema registry url using the
            // schema.registry.url property for redundancy (comma separated list). 
            // The property name is not plural to follow the convention set by
            // the Java implementation.
            MaxCachedSchemas = 10,
            Url = "http://localhost:8081",
            BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo,

        };

        var jsonSerializerConfig = new JsonSerializerConfig
        {
            BufferBytes = 100
        };


        using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
        var jsonSerializer = new JsonSerializer<KafkaSerializer>(schemaRegistry, jsonSerializerConfig);

        var store = new KafkaSerializer
        {
            Name = "This is the Kafka serializer",
            Description = "An example from .NET producer using Kafka serializer",
        };

        var producer = new ProducerBuilder<string, KafkaSerializer>(config)
            .SetValueSerializer(jsonSerializer)
            .Build();

        var result = await producer.ProduceAsync("my_orders", new Message<string, KafkaSerializer> { Key = "k1", Value = store });

        Console.WriteLine($" Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");

        // Flush is an asynchronous method that dumps the message into a Kafka topic.
        // If you dump each message individually you are creating a synchronous producer.
        // So it is better to create a batch of messages and then send them to Kafka.
        // For this example, we have created a synchronous producer.
        producer.Flush();
    }
}
