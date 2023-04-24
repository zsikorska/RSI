using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyCalculator : ICalculator
    {
        public int iAdd(int val1, int val2)
        {
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 + val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    return result;
                }
            }catch(OverflowException e)
            {
                throw new FaultException<OverflowException>(e);
            }
        }

        public int iSub(int val1, int val2)
        {
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 - val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e);
            }
        }

        public int iMul(int val1, int val2)
        {
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 * val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e);
            }
        }

        public int iDiv(int val1, int val2)
        {
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 / val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e);
            }
        }

        public int iMod(int val1, int val2)
        {
            Console.WriteLine("Wywołano dodawanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 % val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e);
            }
        }

        public async Task<(int, int)> CountAndMaxPrime(int lowerBound, int upperBound)
        {
            Console.WriteLine($"...called CountAndMaxPrimesInRangeAsync({lowerBound}, {upperBound})");


            if (upperBound < lowerBound)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(), "Upper bound cannot be smaller than lower bound.");
            }

            if (lowerBound <= 0 || upperBound <= 0)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(), "Lower bound and upper bound must be greater than 0.");
            }

            var isPrime = new bool[upperBound + 1];
            for (var i = 0; i < isPrime.Length; i++)
            {
                isPrime[i] = true;
            }

            isPrime[0] = false;
            isPrime[1] = false;

            for (var p = 2; p * p <= upperBound; p++)
            {
                if (isPrime[p])
                {
                    for (var i = p * p; i <= upperBound; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }

            var count = 0;
            var maxPrime = -1;
            for (var p = lowerBound; p <= upperBound; p++)
            {
                if (isPrime[p])
                {
                    count++;
                    maxPrime = p;
                }
            }

            return (count, maxPrime);

        }
    }
}
