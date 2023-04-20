using System.ComponentModel.Design;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcImageStreamingClient;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Security.Authentication;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

const int BUFFER_SIZE = 2048;

MyData.Info();
using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new ImageStreaming.ImageStreamingClient(channel);

var root = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
var path = Path.Combine(root, "images");
var receivedPath = Path.Combine(root, "received");
await Menu();

async Task Menu()
{
    Console.WriteLine("Wybierz opcję:");
    Console.WriteLine("1) Wysyłanie obrazka do serwera");
    Console.WriteLine("2) Odbieranie obrazka z serwera");
    Console.WriteLine("3) Wyjście z programu\n");

    Console.Write("Opcja: ");
    String option = Console.ReadLine();
    Console.WriteLine();

    if (option == "1")
    {
        Console.Write("Podaj nazwę pliku: ");
        string fileName = Console.ReadLine();

        try
        {
            using (var call = client.SendImageToServer())
            {
                var imageBytesBuffer = new byte[BUFFER_SIZE];
                var counter = 0;
                using (var imageStream = File.OpenRead(Path.Combine(path, fileName + ".jpg")))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, imageBytesRead) };
                        await call.RequestStream.WriteAsync(imageData);
                        counter++;
                        Console.WriteLine($"Wysłano pakiet o numerze {counter}");
                    }
                }
                await call.RequestStream.CompleteAsync();
                await call.ResponseAsync;
                Console.WriteLine("Wysłano " + counter + " pakietów.");
                Console.WriteLine("Wysyłanie zakończone pomyślnie.\n");
            }
        }
        catch
        {
            Console.WriteLine("Nie znaleziono pliku.\n");
            await Menu();
        }
        await Menu();
    }

    else if (option == "2")
    {
        Console.Write("Podaj nazwę pod jaką chcesz zapisać obrazek: ");
        string fileName = Console.ReadLine();

        Console.Write("Podaj nazwę obrazka na serwerze: ");
        string serverFileName = Console.ReadLine() + ".jpg";
        try
        {
            var counter = 0;
            using (var call = client.SendImageToClient(new ImageName { Filename = serverFileName }))
            {
                using (var receivedImageStream = File.Create(Path.Combine(receivedPath, fileName + ".jpg")))
                {
                    while (await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var imageData = call.ResponseStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());
                        counter++;
                        Console.WriteLine($"Odebrano pakiet o numerze {counter}");

                    }
                }
            }
            Console.WriteLine("Odebrano " + counter + " pakietów.");
            Console.WriteLine("Zdjęcie odebrane pomyślnie.\n");
        }
        catch
        {
            Console.WriteLine("Nie znaleziono pliku.\n");
            await Menu();
        }
        await Menu();
    }

    else if (option == "3")
    {
        Environment.Exit(0);
    }

    else
    {
        Console.WriteLine("Podano nieprawidłową opcję.\n");
        await Menu();
    }
}
