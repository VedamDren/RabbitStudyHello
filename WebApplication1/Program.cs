using System;
using System.Text;
using RabbitMQ.Client;


class Program

{
    static void Main(string[] args)
    {
        // ������� ��������� RabbitMqProducer
        var producer = new RabbitMqProducer();

        // ���������� ���������
        string message = "Hello, RabbitMQ!";
        producer.SendMessage(message);

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}

