using System.ComponentModel.Design;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcDistanceClient;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

MyData.Info();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Distance.DistanceClient(channel);

Menu();

void Menu()
{
    Console.WriteLine("Wybierz opcję:");
    Console.WriteLine("1) Dystans do Warszawy");
    Console.WriteLine("2) Dystans pomiędzy dwoma miastami");
    Console.WriteLine("3) Dystans pomiędzy trzema miastami");
    Console.WriteLine("4) Wyjście z programu");
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
        Console.WriteLine("Dystans do Warszawy wynosi " + reply.Distance + " km");
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
        Console.WriteLine("Dystans między miastami wynosi " + reply.Distance + " km");
        Menu();
    }
    else if(option == 3)
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

        Console.Write("(3) Podaj nazwę miasta: ");
        string city3 = Console.ReadLine();
        Console.Write("(3) Podaj szerokość geograficzną: ");
        double lat3 = double.Parse(Console.ReadLine());
        Console.Write("(3) Podaj długość geograficzną: ");
        double lon3 = double.Parse(Console.ReadLine());

        var reply = client.ThreeCityDistance(new ThreeCityRequest { City1 = city1, Lat1 = lat1, Lon1 = lon1, City2 = city2, Lat2 = lat2, Lon2 = lon2, City3 = city3, Lat3 = lat3, Lon3 = lon3 });
        Console.WriteLine("Dystans między miastami wynosi " + reply.Distance + " km");
        Menu();
    }
    else if(option == 4)
    {
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Podano nieprawidłową opcję.");
        Menu();
    }
}
