using Producer;

Console.WriteLine("Kafka Producer .NET - Started");

var example = new Example2();

await example.Execute();

Console.WriteLine("Kafka Producer .NET - Completed");
