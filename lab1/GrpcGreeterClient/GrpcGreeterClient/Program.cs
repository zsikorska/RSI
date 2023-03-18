using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;


MyData.Info();
Console.Write("Podaj imię: ");
var name = Console.ReadLine();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7105");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = name });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
