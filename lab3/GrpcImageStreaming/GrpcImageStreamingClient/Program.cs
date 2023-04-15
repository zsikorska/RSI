using System.ComponentModel.Design;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcImageStreamingClient;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

MyData.Info();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new ImageStreaming.ImageStreamingClient(channel);

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
        Console.Write("(1) Podaj nazwę miasta: ");
        string city1 = Console.ReadLine();
        Console.Write("(1) Podaj szerokość geograficzną: ");
        double lat1 = double.Parse(Console.ReadLine());
        Console.Write("(1) Podaj długość geograficzną: ");
        double lon1 = double.Parse(Console.ReadLine());

        var reply = client.WarsawDistance(new WarsawRequest { City1 = city1, Lat1 = lat1, Lon1 = lon1 });
        Console.WriteLine("Dystans do Warszawy wynosi " + String.Format("{0:0.00}", reply.Distance) + " km");
        Menu();
    }

    else if(option == 2)
    {
        Console.Write("(1) Podaj nazwę miasta: ");
        string city1 = Console.ReadLine();
        Console.Write("(1) Podaj szerokość geograficzną: ");
        double lat1 = double.Parse(Console.ReadLine());
        Console.Write("(1) Podaj długość geograficzną: ");
        double lon1 = double.Parse(Console.ReadLine());

        Console.Write("(2) Podaj nazwę miasta: ");
        string city2 = Console.ReadLine();
        Console.Write("(2) Podaj szerokość geograficzną: ");
        double lat2 = double.Parse(Console.ReadLine());
        Console.Write("(2) Podaj długość geograficzną: ");
        double lon2 = double.Parse(Console.ReadLine());

        var reply = client.TwoCityDistance(new TwoCityRequest { City1 = city1, Lat1 = lat1, Lon1 = lon1, City2 = city2, Lat2 = lat2, Lon2 = lon2 });
        Console.WriteLine("Dystans między miastami wynosi " + String.Format("{0:0.00}", reply.Distance) + " km");
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
