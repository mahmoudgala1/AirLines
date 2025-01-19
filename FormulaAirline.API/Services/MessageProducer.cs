using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirline.API.Services;

public class MessageProducer : IMessageProducer
{
  public async Task SendingMessage<T>(T message)
  {
    var factory = new ConnectionFactory
    {
      HostName = "localhost",
      Port = 5672,
    };
    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();
    await channel.QueueDeclareAsync(queue: "bookings", durable: false, exclusive: false, autoDelete: false,
        arguments: null);
    var jsonString = JsonSerializer.Serialize(message);
    Console.WriteLine(jsonString);
    var body = Encoding.UTF8.GetBytes(jsonString);
    await channel.BasicPublishAsync("", "bookings", body: body);
  }
}