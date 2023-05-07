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
            Console.WriteLine("Zwracanie liczby osób.");
            return persons.Count;
        }

        public List<Person> GetAllPersons()
        {
            Console.WriteLine("Zwracanie wszystkich osób.");
            return persons;
        }

        public Person GetPersonById(int id)
        {
            Console.WriteLine("Zwracanie osoby o id: {0}.", id);
            Person person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                Console.WriteLine("Osoba o id {0} nie znaleziona.", id);
                throw new FaultException("Osoba o podanym id nie istnieje.");
            }
            Console.WriteLine("Osoba o id {0} znaleziona: {1}", id, person.Name);
            return person;
        }

        public Person AddPerson(Person person)
        {
            if (persons.Contains(person))
            {
                throw new FaultException("Osoba już istnieje");
            }

            person.Id = nextId++;
            Console.WriteLine("Dodawanie osoby - id: {0}, imię: {1}, wiek: {2}.", person.Id, person.Name, person.Age);
            persons.Add(person);
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson == null)
            {
                throw new FaultException("Osoba nie istnieje");
            }

            Console.WriteLine("Aktualizowanie osoby - id: {0}, imię: {1}, wiek: {2}.", existingPerson.Id, existingPerson.Name, existingPerson.Age);
            existingPerson.Name = person.Name;
            existingPerson.Age = person.Age;
            return existingPerson;
        }

        public Person DeletePerson(int id)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson == null)
            {
                throw new FaultException("Osoba nie istnieje");
            }

            Console.WriteLine("Usuwanie osoby - id: {0}, imię: {1}, wiek: {2}.", existingPerson.Id, existingPerson.Name, existingPerson.Age);
            persons.Remove(existingPerson);
            return existingPerson;
        }

        public async Task<List<Person>> FilterPersonsByNameAsync(string name)
        {
            Console.WriteLine("Filtrowanie osób po imieniu: {0}.", name);
            List<Person> filteredPersons = new List<Person>();
            foreach (var person in persons)
            {
                if (person.Name.Contains(name))
                {
                    filteredPersons.Add(person);
                }
            }

            await Task.Delay(3000);
            return filteredPersons;
        }


    }
}
