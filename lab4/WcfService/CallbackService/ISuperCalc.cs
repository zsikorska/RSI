using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CallbackService
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISuperCalcCallback))]
    public interface ISuperCalc
    {
        [OperationContract(IsOneWay = true)]
        void Factorial(double n);
        [OperationContract(IsOneWay = true)]
        void DoSomething(int sec);
    }

    public interface ISuperCalcCallback
    {
        [OperationContract(IsOneWay = true)]
        void FactorialResult(double result);
    }

    // Użyj kontraktu danych, jak pokazano w poniższym przykładzie, aby dodać typy złożone do operacji usługi.
    // Możesz dodać pliki XSD do projektu. Po skompilowaniu projektu możesz bezpośrednio użyć zdefiniowanych w nim typów danych w przestrzeni nazw „CallbackService.ContractType”.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
