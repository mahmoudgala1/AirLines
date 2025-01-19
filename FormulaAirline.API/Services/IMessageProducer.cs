namespace FormulaAirline.API.Services;

public interface IMessageProducer
{
  Task SendingMessage<T>(T message);
}