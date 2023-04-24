using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = ProtectionLevel.None)]
    public interface ICalculator
    {
        [OperationContract]
        int iAdd(int val1, int val2);
        [OperationContract]
        int iSub(int val1, int val2);
        [OperationContract]
        int iMul(int val1, int val2);
        [OperationContract]
        int iDiv(int val1, int val2);
        [OperationContract]
        int iMod(int val1, int val2);
        [OperationContract]
        Task<(int, int)> CountAndMaxPrime(int l1, int l2);
    }

    // Użyj kontraktu danych, jak pokazano w poniższym przykładzie, aby dodać typy złożone do operacji usługi.
    // Możesz dodać pliki XSD do projektu. Po skompilowaniu projektu możesz bezpośrednio użyć zdefiniowanych w nim typów danych w przestrzeni nazw „WcfService.ContractType”.
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
