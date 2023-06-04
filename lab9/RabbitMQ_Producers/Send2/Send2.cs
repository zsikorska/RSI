using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Send2;

MyData.Info();

int timeX = 15;
int interval = 5;

// var factory = new ConnectionFactory { HostName = "10.182.154.188" };
var factory = new ConnectionFactory { HostName = "localhost" };
//factory.Port = 5672;
//factory.UserName = "user";
//factory.Password = "123";
//factory.VirtualHost = "/";
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

DateTime start = DateTime.Now;

int i = 0;
while ((DateTime.Now - start).TotalMilliseconds < timeX * 1000)
{
    var message = JsonConvert.SerializeObject(new { text = "Zuza", time = DateTime.Now.ToString("hh:mm:ss"), number = i });
    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: Encoding.UTF8.GetBytes(message));

    Console.WriteLine($" [x] Sent {message}");
    Thread.Sleep(new Random().Next(interval) * 1000);
    i++;
}

channel.BasicPublish(exchange: string.Empty,
                     routingKey: "hello",
                     basicProperties: null,
                     body: Encoding.UTF8.GetBytes("Zuza ended producing"));


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();