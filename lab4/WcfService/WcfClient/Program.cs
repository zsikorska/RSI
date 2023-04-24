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
            Console.WriteLine("... The client is started");
            Uri baseAddress = new Uri("http://localhost:5000/WcfService/endpoint1");
            BasicHttpBinding myBinding = new BasicHttpBinding();
            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            ChannelFactory<ServiceReference1.ICalculator> myCF = new ChannelFactory<ServiceReference1.ICalculator>(myBinding, eAddress);
            ServiceReference1.ICalculator myClient = myCF.CreateChannel();

            myClient2 = new CalculatorClient("WSHttpBinding_ICalculator");


            Console.WriteLine("...press <ENTER> to STOP client...");
            Console.WriteLine();
            Console.ReadLine();
            ((IClientChannel)myClient).Close();
            Console.WriteLine("...Client closed - FINISHED");
        }


        static void Menu()
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Modulo");
            Console.WriteLine("6. Count and find max prime numbers in range");
            Console.WriteLine("0. Exit");
            int num = InputNumber();
            if(num == 1)
            {
                var result = myClient2.iAdd(1, 2);
            }
            else if (num == 2)
            {

            }
            else if (num == 3)
            {

            }
            else if (num == 4)
            {

            }
            else if (num == 5)
            {

            }
            else if (num == 6)
            {

            }
            else if (num == 0)
            {
                Task<(int, int)> asyncResult = myClient2.CountAndMaxPrimeAsync(100000, 1000000);
                (int count, int max) = asyncResult.Result;
                Console.WriteLine("Count" + count);
                Console.WriteLine("max" + max);
            }
            else
            {
                Console.WriteLine("Niewłaściwa opcja");
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
            int n1 = InputNumber();
            int n2 = InputNumber();
            return (n1, n2);
        }
    }
}
