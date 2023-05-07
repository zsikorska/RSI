using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
        Person UpdatePerson(int id, Person person);

        [OperationContract]
        Person DeletePerson(int id);

        [OperationContract]
        Task<List<Person>> FilterPersonsByNameAsync(string name);
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
