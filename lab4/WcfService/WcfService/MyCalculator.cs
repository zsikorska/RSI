using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyCalculator : ICalculator
    {
        public double Add(double val1, double val2)
        {
            double result = val1 + val2;
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            Console.WriteLine("Wynik wywołania: {0}", result);
            return result;
        }
        public double Multiply(double val1, double val2)
        {
            double result = val1 * val2;
            Console.WriteLine("Wywołano mnożenie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            Console.WriteLine("Wynik wywołania: {0}", result);
            return result;
        }
        public double HMultiply(double val1, double val2)
        {
            double result = val1 * val2;
            Console.WriteLine("Wywołano mnożenie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            Thread.Sleep(5000);
            Console.WriteLine("Wynik wywołania: {0}", result);
            return result;
        }
    }
}
