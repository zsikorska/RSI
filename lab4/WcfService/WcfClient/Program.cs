using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    internal class Program
    {
        static CalculatorClient myClient2;

        static void Main(string[] args)
        {
            MyData.Info();

            Uri baseAddress = new Uri("http://localhost:5000/WcfService/endpoint1");
            BasicHttpBinding myBinding = new BasicHttpBinding();
            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            ChannelFactory<ServiceReference1.ICalculator> myCF = new ChannelFactory<ServiceReference1.ICalculator>(myBinding, eAddress);
            ServiceReference1.ICalculator myClient = myCF.CreateChannel();

            myClient2 = new CalculatorClient("WSHttpBinding_ICalculator");

            Menu();
        }


        static void Menu()
        {
            int num = 0;
            while (num != 7)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Subtraction");
                Console.WriteLine("3. Multiplication");
                Console.WriteLine("4. Division");
                Console.WriteLine("5. Modulo");
                Console.WriteLine("6. Count and find max prime numbers in range");
                Console.WriteLine("7. Exit");
                Console.WriteLine();
                Console.Write("Your choice: ");
                num = InputNumber();
                Console.WriteLine();

                if (num == 1)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        var result = myClient2.iAdd(num1, num2);
                        Console.WriteLine($"{num1} + {num2} = {result}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 2)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        var result = myClient2.iSub(num1, num2);
                        Console.WriteLine($"{num1} - {num2} = {result}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 3)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        var result = myClient2.iMul(num1, num2);
                        Console.WriteLine($"{num1} * {num2} = {result}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 4)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        var result = myClient2.iDiv(num1, num2);
                        Console.WriteLine($"{num1} / {num2} = {result}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 5)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        var result = myClient2.iMod(num1, num2);
                        Console.WriteLine($"{num1} % {num2} = {result}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 6)
                {
                    try
                    {
                        (int num1, int num2) = InputNumbers();
                        (int count, int max) = myClient2.CountAndMaxPrimeAsync(num1, num2).Result;
                        Console.WriteLine($"Liczba liczb pierwszych z zakresu [{num1}, {num2}]: " + count);
                        Console.WriteLine($"Najwieksza liczba pierwsza z zakresu [{num1}, {num2}]: " + max);

                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 7)
                {
                    myClient2.Close();
                }
                else
                {
                    Console.WriteLine("Niewłaściwa opcja");
                    Console.WriteLine();
                    Menu();
                }
            }

        }

        static int InputNumber()
        {
            int number;
            number = int.Parse(Console.ReadLine());
            return number;
        }

        static (int, int) InputNumbers()
        {
            Console.Write("Podaj pierwszą liczbę: ");
            int n1 = InputNumber();
            Console.Write("Podaj drugą liczbę: ");
            int n2 = InputNumber();
            Console.WriteLine();
            return (n1, n2);
        }

    }
}
