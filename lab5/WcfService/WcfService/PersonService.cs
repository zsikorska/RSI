﻿using System;
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
            Console.WriteLine();
            return persons.Count;
        }

        public List<Person> GetAllPersons()
        {
            Console.WriteLine("Zwracanie wszystkich osób.");
            Console.WriteLine();
            return persons;
        }

        public Person GetPersonById(int id)
        {
            Console.WriteLine("Zwracanie osoby o id: {0}.", id);
            Person person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                Console.WriteLine("Osoba o id {0} nie znaleziona.", id);
                Console.WriteLine();
                throw new FaultException("Osoba o podanym id nie istnieje.");
            }
            Console.WriteLine("Osoba o id {0} znaleziona: {1}", id, person.Name);
            Console.WriteLine();
            return person;
        }

        public Person AddPerson(Person person)
        {
            if (EmailInDB(person.Email))
            {
                Console.WriteLine("Nie można dodać osoby bo jest o takim emailu w systemie.");
                Console.WriteLine();
                throw new FaultException("Nie można dodać osoby bo jest o takim emailu w systemie.");
            }

            person.Id = nextId++;
            Console.WriteLine("Dodawanie osoby - id: {0}, imię: {1}, wiek: {2}, email: {3}.", person.Id, person.Name, person.Age, person.Email);
            Console.WriteLine();
            persons.Add(person);
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson == null)
            {
                Console.WriteLine("Nie można zaktualizować osoby, bo taka osoba nie istnieje.");
                Console.WriteLine();
                throw new FaultException("Nie można zaktualizować osoby, bo taka osoba nie istnieje.");
            }
            if (EmailInDBInOtherUser(person.Email, person.Id))
            {
                Console.WriteLine("Nie można zmienić emmailu, ponieważ jest już wykorzystywany przez innego użytkownika.");
                Console.WriteLine();
                throw new FaultException("Nie można zmienić emmailu, ponieważ jest już wykorzystywany przez innego użytkownika.");
            }

            existingPerson.Name = person.Name;
            existingPerson.Age = person.Age;
            existingPerson.Email = person.Email;
            Console.WriteLine("Aktualizowanie osoby - id: {0}, imię: {1}, wiek: {2}, email: {3}.", existingPerson.Id, existingPerson.Name, existingPerson.Age, existingPerson.Email);
            Console.WriteLine();
            return existingPerson;
        }

        public Person DeletePerson(int id)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson == null)
            {
                Console.WriteLine("Nie można usunąć osoby, bo taka osoba nie istnieje.");
                Console.WriteLine();
                throw new FaultException("Nie można usunąć osoby, bo taka osoba nie istnieje.");
            }

            Console.WriteLine("Usuwanie osoby - id: {0}, imię: {1}, wiek: {2},  email: {3}.", existingPerson.Id, existingPerson.Name, existingPerson.Age, existingPerson.Email);
            Console.WriteLine();
            persons.Remove(existingPerson);
            return existingPerson;
        }

        public async Task<List<Person>> FilterPersonsByNameAsync(string name)
        {
            Console.WriteLine("Filtrowanie osób po imieniu: {0}.", name);
            Console.WriteLine();
            List<Person> filteredPersons = new List<Person>();
            string searchName = name.ToLower();

            foreach (var person in persons)
            {
                if (person.Name.ToLower().Contains(searchName))
                {
                    filteredPersons.Add(person);
                }
            }

            await Task.Delay(4000);
            Console.WriteLine("Zakończono filtrowanie osób po imieniu: {0}.", name);
            return filteredPersons;
        }

        private bool EmailInDB(String email)
        {
            foreach(var person in persons)
            {
                if (person.Email.ToLower().Equals(email))
                {
                    return true;
                }
            }
            return false;
        }

        private bool EmailInDBInOtherUser(String email, int id)
        {
            foreach (var person in persons)
            {
                if (person.Email.ToLower().Equals(email) && id != person.Id)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
