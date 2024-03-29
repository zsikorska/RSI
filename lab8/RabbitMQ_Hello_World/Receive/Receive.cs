﻿using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receive;

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

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(queue: "hello",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
