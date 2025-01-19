using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory
{
  HostName = "localhost",
  Port = 5672,
  UserName = "guest",
  Password = "guest",
};
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync(queue: "bookings", durable: false, exclusive: false, autoDelete: false,
    arguments: null);
var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, eventArgs) =>
{
  var body = eventArgs.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);
  Console.WriteLine($"New ticket processing is initiated for - {message}");
  return Task.CompletedTask;
};
await channel.BasicConsumeAsync("bookings", true, consumer);
Console.ReadKey();
