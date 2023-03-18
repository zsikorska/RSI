﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GrpcGreeterClient
{
    internal class MyData
    {
        public static void Info()
        {
            Console.WriteLine("stud1, index1");
            Console.WriteLine("stud2, index2");
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
}
