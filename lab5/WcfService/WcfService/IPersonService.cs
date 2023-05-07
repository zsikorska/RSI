using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = ProtectionLevel.None)]
    public interface IPersonService
    {
        [OperationContract]
        int GetPersonsCount();

        [OperationContract]
        List<Person> GetAllPersons();

        [OperationContract]
        Person GetPersonById(int id);

        [OperationContract]
        Person AddPerson(Person person);

        [OperationContract]
        Person UpdatePerson(Person person);

        [OperationContract]
        Person DeletePerson(int id);

        [OperationContract]
        List<Person> FilterPersonsByName(string name);
    }

    // Użyj kontraktu danych, jak pokazano w poniższym przykładzie, aby dodać typy złożone do operacji usługi.
    [DataContract]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}
