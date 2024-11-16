using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMqConsumer
{
    private readonly string _hostname;
    private readonly string _queueName;

    // Конструктор класса, принимающий хост и имя очереди
    public RabbitMqConsumer(string hostname = "localhost", string queueName = "hello")
    {
        _hostname = hostname;
        _queueName = queueName;
    }

    // Метод для получения сообщений из очереди
    public void StartListening()
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Объявляем очередь, если она не существует
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Настроим обработчик событий для получения сообщений
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] Received: {message}");

                    // Здесь можно добавить логику для обработки сообщения
                };

                // Начинаем получать сообщения из очереди
                channel.BasicConsume(queue: _queueName,
                                     autoAck: true, // Подтверждаем получение сообщений автоматически
                                     consumer: consumer);

                Console.WriteLine("[*] Waiting for messages. To exit press [Enter]");
                Console.ReadLine(); // Ожидаем нажатия клавиши для завершения
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}