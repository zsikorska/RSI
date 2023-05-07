using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PersonService : IPersonService
    {
        private List<Person> persons = new List<Person>();
        private int nextId = 1;


        public int GetPersonsCount()
        {
            Console.WriteLine("Getting person count.");
            return persons.Count;
        }

        public List<Person> GetAllPersons()
        {
            Console.WriteLine("Getting all persons.");
            return persons;
        }

        public Person GetPersonById(int id)
        {
            Console.WriteLine("Getting person by ID: {0}.", id);
            Person person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                Console.WriteLine("User {0} not found.", id);
                throw new ArgumentException("Person does not exist.");
            }
            Console.WriteLine("User {0} found in database: {1}", id, person.Name);
            return person;
        }

        public Person AddPerson(Person person)
        {
            if (persons.Contains(person))
            {
                throw new ArgumentException("Person already exists.");
            }

            person.Id = nextId++;
            Console.WriteLine("Adding person with ID: {0}, Name: {1}, Age: {2}.", person.Id, person.Name, person.Age);
            persons.Add(person);
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson == null)
            {
                throw new ArgumentException("Person does not exist.");
            }

            Console.WriteLine("Updating person with ID: {0}, Name: {1}, Age: {2}.", existingPerson.Id, existingPerson.Name, existingPerson.Age);
            existingPerson.Name = person.Name;
            existingPerson.Age = person.Age;
            return existingPerson;
        }

        public Person DeletePerson(int id)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson == null)
            {
                throw new ArgumentException("Person does not exist.");
            }

            Console.WriteLine("Deleting person with ID: {0}, Name: {1}, Age: {2}.", existingPerson.Id, existingPerson.Name, existingPerson.Age);
            persons.Remove(existingPerson);
            return existingPerson;
        }

        public List<Person> FilterPersonsByName(string name)
        {
            Console.WriteLine("Filtering persons by name: {0}.", name);
            return persons.Where(p => p.Name.Contains(name)).ToList();
        }


    }
}
