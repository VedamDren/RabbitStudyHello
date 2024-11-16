using RabbitMQ.Client;
using System;
using System.Text;

public class RabbitMqProducer
{
    private readonly string _hostname;
    private readonly string _queueName;

    public RabbitMqProducer(string hostname = "localhost", string queueName = "hello")
    {
        _hostname = hostname;
        _queueName = queueName;
    }

    // Метод для отправки сообщения в очередь
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = _hostname };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Объявляем очередь, если она еще не существует
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Преобразуем сообщение в байты
            var body = Encoding.UTF8.GetBytes(message);

            // Отправляем сообщение в очередь
            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"[x] Sent: {message}");
        }
    }
}