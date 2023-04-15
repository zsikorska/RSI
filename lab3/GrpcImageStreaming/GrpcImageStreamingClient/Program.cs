using System.ComponentModel.Design;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcImageStreamingClient;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

MyData.Info();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new ImageStreaming.ImageStreamingClient(channel);

var path = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreamingClient\\images\\";
var receivedPath = "E:\\STUDIA\\6sem\\rsi\\laby\\RSI\\lab3\\GrpcImageStreaming\\GrpcImageStreamingClient\\received\\";

Menu();

void Menu()
{
    Console.WriteLine("Wybierz opcję:");
    Console.WriteLine("1) Wysyłanie obrazka do serwera");
    Console.WriteLine("2) Odbieranie obrazka z serwera");
    Console.WriteLine("3) Wyjście z programu");

    Console.Write("Opcja: ");
    int option = int.Parse(Console.ReadLine());

    if (option == 1)
    {
        Console.Write("Podaj nazwę pliku: ");
        string fileName = Console.ReadLine();

        try
        {
            using (var call = client.SendImageToServer())
            {
                var imageBytesBuffer = new byte[256];
                using (var imageStream = File.OpenRead(path + fileName))
                {
                    int imageBytesRead;
                    while ((imageBytesRead = await imageStream.ReadAsync(imageBytesBuffer, 0, imageBytesBuffer.Length)) > 0)
                    {
                        var imageData = new ImageData { Data = ByteString.CopyFrom(imageBytesBuffer, 0, imageBytesRead) };
                        await call.RequestStream.WriteAsync(imageData);
                    }
                }
                await call.RequestStream.CompleteAsync();
                Console.WriteLine("Wysyłanie zakończone pomyślnie.");
            }
        }
        catch
        {
            Console.WriteLine("Nie znaleziono pliku.");
            Menu();
        }
        Menu();
    }

    else if(option == 2)
    {
        Console.Write("Podaj nazwę pod jaką chcesz zapisać obrazek: ");
        string fileName = Console.ReadLine();

        try
        {
            using (var call = client.SendImageToClient())
            {
                using (var receivedImageStream = File.Create(receivedPath + fileName)
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var imageData = call.ResponseStream.Current;
                        await receivedImageStream.WriteAsync(imageData.Data.ToByteArray());

                    }
                }
            }
            Console.WriteLine("Zdjęcie odebrane pomyślnie.")
        }
        catch
        {
            Console.WriteLine("Nie znaleziono pliku.");
            Menu();
        }
        Menu();
    }

    else if(option == 3)
    {
        Environment.Exit(0);
    }

    else
    {
        Console.WriteLine("Podano nieprawidłową opcję.");
        Menu();
    }
}
