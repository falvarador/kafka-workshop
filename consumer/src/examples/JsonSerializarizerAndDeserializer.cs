using Confluent.Kafka;
using System.Text.Json;

namespace Consumer;

public class CustomJsonSerializer<T>(JsonSerializerOptions defaultOptions) : ISerializer<T>
{
    private JsonSerializerOptions DefaultOptions { get; } = defaultOptions;

    public byte[] Serialize(T data, SerializationContext context)
        => JsonSerializer.SerializeToUtf8Bytes(data, DefaultOptions);
}

public class CustomJsonDeserializer<T>(JsonSerializerOptions defaultOptions) : IDeserializer<T>
{
    private JsonSerializerOptions DefaultOptions { get; } = defaultOptions;

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        => JsonSerializer.Deserialize<T>(data, DefaultOptions)!;
}
