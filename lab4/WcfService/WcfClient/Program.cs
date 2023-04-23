using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;
using WcfClient.ServiceReference2;
using WcfService;

namespace WcfClient
{
    internal class Program
    {
        static CalculatorClient myClient2;
        static void Main(string[] args)
        {
            Console.WriteLine("... The client is started");
            Uri baseAddress;
            BasicHttpBinding myBinding = new BasicHttpBinding();
            baseAddress = new Uri("http://localhost:5000/WcfService/endpoint1");
            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            ChannelFactory<ServiceReference1.ICalculator> myCF = new ChannelFactory<ServiceReference1.ICalculator>(myBinding, eAddress);
            ServiceReference1.ICalculator myClient = myCF.CreateChannel();

            Console.Write("...calling Add (for entpoint1) ");
            double result2 = myClient.Add(-3.7, 9.5);
            Console.WriteLine("Result = " + result2);


            myClient2 = new CalculatorClient("WSHttpBinding_ICalculator");
            Console.Write("...calling Multiply (for endpoint2) - ");
            double result1 = myClient2.Multiply(-3.7, 9.5);
            Console.WriteLine("Result = " + result1);

            Console.WriteLine("2...calling HMultiply ASYNCHRONOUSLY !!!");
            Task<double> asyncResult = callHMultiplyAsync(1.1, -3.3);
            Thread.Sleep(100);

            SuperCalcCallback myCbHandler = new SuperCalcCallback();
            InstanceContext instanceContext = new InstanceContext(myCbHandler);
            SuperCalcClient myClient3 = new SuperCalcClient(instanceContext);
            double value1 = 10;
            Console.WriteLine("...calling Factorial({0})...", value1);
            myClient3.Factorial(value1);

            Console.Write("...calling Add (for endpoint2) - ");
            double addResult = myClient2.Add(99, 11);
            Console.WriteLine("Result = " + addResult);

            double aResult = asyncResult.Result;
            Console.WriteLine("2...HMultiplyAsync Result = " + aResult);

            Console.WriteLine("...press <ENTER> to STOP client...");
            Console.WriteLine();
            Console.ReadLine();
            ((IClientChannel)myClient).Close();
            myClient3.Close();
            Console.WriteLine("CLIENT3 - STOP");
            Console.WriteLine("...Client closed - FINISHED");
        }

        static async Task<double> callHMultiplyAsync(double n1, double n2)
        {
            Console.WriteLine("2......called callHMultiplyAsync");
            double reply = await myClient2.HMultiplyAsync(n1, n2);
            Console.WriteLine("2......finished HMultipleAsync");
            return reply;
        }
    }
}
