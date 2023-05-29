using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using Send2;

MyData.Info();

var factory = new ConnectionFactory { HostName = "localhost" };
factory.Port = 5672;
factory.UserName = "user";
factory.Password = "123";
factory.VirtualHost = "/";

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

int maxSeconds = 6;

for (int i = 0; i < 5; i++)
{
    var message = $"Zuzanna msg_number: {i}";
    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: Encoding.UTF8.GetBytes(message));

    Console.WriteLine($" [x] Sent {message}");
    Thread.Sleep(new Random().Next(maxSeconds) * 1000);
}


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
