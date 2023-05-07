using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;
using System.Collections;
using System.Collections.Generic;

namespace WcfClient
{
    internal class Program
    {
        static PersonServiceClient myClient2;

        static void Main(string[] args)
        {
            MyData.Info();

            Uri baseAddress = new Uri("http://localhost:5000/WcfService/endpoint1");
            BasicHttpBinding myBinding = new BasicHttpBinding();
            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            ChannelFactory<ServiceReference1.IPersonService> myCF = new ChannelFactory<ServiceReference1.IPersonService>(myBinding, eAddress);
            ServiceReference1.IPersonService myClient = myCF.CreateChannel();

            myClient2 = new PersonServiceClient("WSHttpBinding_IPersonService");

            Menu();
        }


        static async void Menu()
        {
            int num = 0;
            while (num != 8)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1) Zwróć liczbę osób");
                Console.WriteLine("2) Zwróć wszystkie osoby");
                Console.WriteLine("3) Zwróć osobę po id");
                Console.WriteLine("4) Dodaj osobę");
                Console.WriteLine("5) Zaktualizuj osobę");
                Console.WriteLine("6) Usuń osobę");
                Console.WriteLine("7) Filtruj osoby po imieniu");
                Console.WriteLine("8) Wyjście \n");
                Console.Write("Twój wybór: ");
                num = InputNumber();
                Console.WriteLine();

                if (num == 1)
                {
                    try
                    {
                        var size = myClient2.GetPersonsCount();
                        Console.WriteLine($"Liczba osób: {size}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 2)
                {
                    try
                    {
                        var persons = myClient2.GetAllPersons();
                        foreach (var person in persons)
                        {
                            Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                        }
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 3)
                {
                    try
                    {
                        Console.Write("Podaj id: ");
                        var id = InputNumber();
                        var person = myClient2.GetPersonById(id);
                        Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 4)
                {
                    try
                    {
                        Console.Write("Podaj imię: ");
                        var name = Console.ReadLine();
                        Console.Write("Podaj wiek: ");
                        var age = InputNumber();
                        var person = myClient2.AddPerson(new Person { Name = name, Age = age });
                        Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 5)
                {
                    try
                    {
                        Console.Write("Podaj id: ");
                        var id = InputNumber();
                        Console.Write("Podaj imię: ");
                        var name = Console.ReadLine();
                        Console.Write("Podaj wiek: ");
                        var age = InputNumber();
                        var person = myClient2.UpdatePerson(new Person { Id = id, Name = name, Age = age });
                        Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 6)
                {
                    try
                    {
                        Console.Write("Podaj id: ");
                        var id = InputNumber();
                        var person = myClient2.DeletePerson(id);
                        Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                    }
                    catch (FaultException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (num == 7)
                {
                    FilterByName();
                    Console.WriteLine();
                }
                else if (num == 8)
                {
                    myClient2.Close();
                }
                else
                {
                    Console.WriteLine("Niewłaściwa opcja");
                    Console.WriteLine();
                    Menu();
                }
            }

        }

        static async void FilterByName()
        {
            try
            {
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();
                List<Person> persons = await myClient2.FilterPersonsByNameAsync(name);
                foreach (var person in persons)
                {
                    Console.WriteLine($"Id: {person.Id}, Imię: {person.Name}, Wiek: {person.Age}");
                }

            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int InputNumber()
        {
            try {
                int number;
                number = int.Parse(Console.ReadLine());
                return number;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błędny numer");
                return InputNumber();
            }
        }


    }
}
