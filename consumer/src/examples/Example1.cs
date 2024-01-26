using Confluent.Kafka;

namespace Consumer;

public class Example1
{
    public static void Execute()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "127.0.0.1:9092",
            GroupId = "demo-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, string>(config).Build();

        consumer.Subscribe("my_orders");
        var cancellationTokenSource = new CancellationTokenSource();

        while (true)
        {
            try
            {
                var cr = consumer.Consume(cancellationTokenSource.Token);
                var message = cr.Message.Value;
                Console.WriteLine($"Consumed message '{message}' at: '{cr.TopicPartitionOffset}'.");
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Consume error: {e.Error.Reason}");
            }
        }
    }
}
