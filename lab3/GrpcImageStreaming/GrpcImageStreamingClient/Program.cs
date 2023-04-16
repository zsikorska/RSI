using System.ComponentModel.Design;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcImageStreamingClient;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Security.Authentication;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

MyData.Info();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new ImageStreaming.ImageStreamingClient(channel);

var path = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreamingClient\\images\\";
var receivedPath = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreamingClient\\received\\";

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
                var imageBytesBuffer = new byte[256];
                using (var imageStream = File.OpenRead(path + fileName + ".jpg"))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, imageBytesRead) };
                        await call.RequestStream.WriteAsync(imageData);
                    }
                }
                await call.RequestStream.CompleteAsync();
                await call.ResponseAsync;
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

    else if(option == "2")
    {
        Console.Write("Podaj nazwę pod jaką chcesz zapisać obrazek: ");
        string fileName = Console.ReadLine();

        try
        {
            using (var call = client.SendImageToClient(new Empty()))
            {
                using (var receivedImageStream = File.Create(receivedPath + fileName + ".jpg"))
                {
                    while (await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var imageData = call.ResponseStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());

                    }
                }
            }
            Console.WriteLine("Zdjęcie odebrane pomyślnie.\n");
        }
        catch
        {
            Console.WriteLine("Nie znaleziono pliku.\n");
            await Menu();
        }
        await Menu();
    }

    else if(option == "3")
    {
        Environment.Exit(0);
    }

    else
    {
        Console.WriteLine("Podano nieprawidłową opcję.\n");
        await Menu();
    }
}
