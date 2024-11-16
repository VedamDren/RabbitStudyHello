using System;

class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляр RabbitMqConsumer
        var consumer = new RabbitMqConsumer();

        // Запускаем прослушивание сообщений
        consumer.StartListening();

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}