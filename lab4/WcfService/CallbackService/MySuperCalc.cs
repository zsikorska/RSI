﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CallbackService
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „Service1” w kodzie i pliku konfiguracji.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MySuperCalc : ISuperCalc
    {
        double result;
        ISuperCalcCallback callback = null;
        public MySuperCalc()
        {
            callback = OperationContext.Current.GetCallbackChannel
            <ISuperCalcCallback>();
        }

        public void DoSomething(int sec)
        {
            throw new NotImplementedException();
        }

        public void Factorial(double n)
        {
            Console.WriteLine("...called Factorial({0})", n);
            Thread.Sleep(1000);
            result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            callback.FactorialResult(result);
        }
    }
}
