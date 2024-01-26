using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace Consumer;

public class CustomJsonAsyncSerializer<T>(JsonSerializerOptions defaultOptions) : IAsyncSerializer<T>
{
    private JsonSerializerOptions DefaultOptions { get; } = defaultOptions;

    public async Task<byte[]> SerializeAsync(T data, SerializationContext context)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, data, DefaultOptions);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        return Encoding.UTF8.GetBytes(await reader.ReadToEndAsync());
    }
}

public class CustomJsonAsyncDeserializer<T>(JsonSerializerOptions defaultOptions) : IAsyncDeserializer<T>
{
    private JsonSerializerOptions DefaultOptions { get; } = defaultOptions;

    public Task<T> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
    {
        // var utf8Reader = new Utf8JsonReader(data.Span);
        // return JsonSerializer.DeserializeAsync<T>(ref utf8Reader, DefaultOptions);

        throw new NotImplementedException();
    }
}
