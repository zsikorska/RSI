using System;
using System.Globalization;
using System.Net.Sockets;
using System.Net;

public class MyData
{
    public static void Info()
    {
        Console.WriteLine("Piotr Łazik, 260371");
        Console.WriteLine("Zuzanna Sikorska, 260464");
        Console.WriteLine(DateTime.Now.ToString("dd MMMM, HH:mm:ss", new CultureInfo("pl-PL")));

        Console.WriteLine(Environment.Version);
        Console.WriteLine(Environment.UserName);
        Console.WriteLine(Environment.OSVersion);

        Console.WriteLine(GetLocalIPAddress());

        Console.WriteLine();

    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}
