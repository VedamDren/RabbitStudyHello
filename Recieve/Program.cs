using System;

class Program
{
    static void Main(string[] args)
    {
        // ������� ��������� RabbitMqConsumer
        var consumer = new RabbitMqConsumer();

        // ��������� ������������� ���������
        consumer.StartListening();

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}