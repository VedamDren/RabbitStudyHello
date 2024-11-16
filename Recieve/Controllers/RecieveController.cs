using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",  // Имя очереди
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Создаем потребителя
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Получено сообщение: {message}");
            };

            // Подключаем потребителя к очереди
            channel.BasicConsume(queue: "hello",  // Очередь, из которой мы получаем сообщения
                                 autoAck: true,   // Автоматическое подтверждение получения сообщений
                                 consumer: consumer);  // Указываем нашего потребителя

            Console.WriteLine("Ожидаю сообщений. Для выхода нажмите [Ctrl+C]");
            Console.ReadLine();
        }
    }
}