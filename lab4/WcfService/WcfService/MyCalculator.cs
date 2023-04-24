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
                    Console.WriteLine();
                    return result;
                }
            }catch(OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Błąd przepełnienia: {val1} + {val2}");
            }
        }

        public int iSub(int val1, int val2)
        {
            Console.WriteLine("Wywołano odejmowanie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 - val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    Console.WriteLine();
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Błąd przepełnienia: {val1} - {val2}");
            }
        }

        public int iMul(int val1, int val2)
        {
            Console.WriteLine("Wywołano mnożenie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 * val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    Console.WriteLine();
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Błąd przepełnienia: {val1} * {val2}");
            }
        }

        public int iDiv(int val1, int val2)
        {
            Console.WriteLine("Wywołano dzielenie");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 / val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    Console.WriteLine();
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Błąd przepełnienia: {val1} / {val2}");
            }
            catch (DivideByZeroException e)
            {
                throw new FaultException<DivideByZeroException>(e, $"Błąd dzielenia przez 0: {val1} / {val2}");
            }
        }

        public int iMod(int val1, int val2)
        {
            Console.WriteLine("Wywołano modulo");
            Console.WriteLine("Parametry wywołania: {0}, {1}", val1, val2);
            try
            {
                checked
                {
                    int result = val1 % val2;
                    Console.WriteLine("Wynik wywołania: {0}", result);
                    Console.WriteLine();
                    return result;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Błąd przepełnienia: {val1} % {val2}");
            }
            catch (DivideByZeroException e)
            {
                throw new FaultException<DivideByZeroException>(e, $"Błąd dzielenia przez 0: {val1} % {val2}");
            }
        }

        public async Task<(int, int)> CountAndMaxPrime(int lowerBound, int upperBound)
        {
            Console.WriteLine($"Wywołano liczenie liczb pierwszych z zakresu [{lowerBound}, {upperBound}]");


            if (upperBound < lowerBound)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(), "Górna granica zakresu nie może byc mniejsza niż dolna.");
            }

            if (lowerBound <= 0 || upperBound <= 0)
            {
                throw new FaultException<ArgumentException>(new ArgumentException(), "Dolna i górna granica zakresu muszą być większe od 0.");
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

            Console.WriteLine($"Liczba liczb pierwszych w zakresie [{lowerBound}, {upperBound}]: {count}");
            Console.WriteLine($"Największa liczba pierwsza w zakresie [{lowerBound}, {upperBound}]: {maxPrime}");
            Console.WriteLine();
            return (count, maxPrime);

        }
    }
}
