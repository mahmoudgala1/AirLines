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
    var con = await factory.CreateConnectionAsync();
    var channel = await con.CreateChannelAsync();
    await channel.QueueDeclareAsync("bookings", durable: true, exclusive: true);
    var jsonString = JsonSerializer.Serialize(message);
    var body = Encoding.UTF8.GetBytes(jsonString);
    await channel.BasicPublishAsync("", "Bookings", body: body);
  }
}