using System.Text;
using Newtonsoft.Json;
class Client
{
    static string uri = "http://localhost:2119/MyRestService.svc";
    static void Main(string[] args)
    {
        MyData.Info();
        Menu();
    }

    static string countPersonsJson()
    {
        return getFromEndpoint("/json/persons/size");
    }

    static string allPersonsJson()
    {
        return getFromEndpoint("/json/persons");
    }

    static string getPersonById(int id)
    {
        return getFromEndpoint("/json/persons/"+id);
    }

    static string getPersonByName(string name)
    {
        return getFromEndpoint("/json/persons/name/"+name);
    }

    static string getFromEndpoint(string endpoint)
    {
        HttpClient client = new HttpClient();
        var response = client.GetAsync(uri + endpoint).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string addNewPerson(int id, string name, int age, string email)
    {
        string endpoint = "/json/persons";
        HttpClient client = new HttpClient();
        StringContent content = new StringContent(JsonConvert.SerializeObject(new Person(id, name, age, email)), Encoding.UTF8, "application/json");
        var response = client.PostAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
    static string editPerson(int id, string name, int age, string email)
    {
        string endpoint = "/json/persons";
        HttpClient client = new HttpClient();
        StringContent content = new StringContent(JsonConvert.SerializeObject(new Person(id, name, age, email)), Encoding.UTF8, "application/json");
        var response = client.PutAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
    static string deletePerson(int id)
    {
        string endpoint = "/json/persons/";
        HttpClient client = new HttpClient();
        var response = client.DeleteAsync(uri + endpoint+id).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
    static void Menu()
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
                Console.WriteLine($"Liczba osób: {countPersonsJson()}");
            }
            else if (num == 2)
            {
                Console.WriteLine($"Lista wszystkich osób: \n{allPersonsJson()}\n");
            }
            else if (num == 3)
            {
                Console.Write("Podaj id: ");
                var id = InputNumber();
                Console.WriteLine($"Osoba o podanym id: \n{getPersonById(id)}\n");
            }
            else if (num == 4)
            {
                Console.Write("Podaj id: ");
                var id = InputNumber();
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();
                Console.Write("Podaj wiek: ");
                var age = InputNumber();
                Console.Write("Podaj email: ");
                var email = Console.ReadLine();
                Console.WriteLine($"\n{addNewPerson(id, name, age, email)}\n");
            }
            else if (num == 5)
            {
                Console.Write("Podaj id: ");
                var id = InputNumber();
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();
                Console.Write("Podaj wiek: ");
                var age = InputNumber();
                Console.Write("Podaj email: ");
                var email = Console.ReadLine();
                Console.WriteLine($"\n{editPerson(id, name, age, email)}\n");
            }
            else if (num == 6)
            {
                Console.Write("Podaj id do usunięcia: ");
                var id = InputNumber();
                Console.WriteLine($"Osoba o podanym id: \n{deletePerson(id)}\n");
            }
            else if (num == 7)
            {
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();
                Console.WriteLine($"Osoba o podanym id: \n{getPersonByName(name)}\n");
            }
            else if (num == 8)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Niewłaściwa opcja\n");
                Menu();
            }
        }

    }

    static int InputNumber()
    {
        try
        {
            int number;
            number = int.Parse(Console.ReadLine());
            return number;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błędny numer. Wpisz numer jeszcze raz.");
            Console.Write("Poprawny numer: ");
            return InputNumber();
        }
    }

}

class Person
{
    public Person(int id, string name, int age, string email)
    {
        Id = id;   
        Name = name;    
        Age = age;
        Email = email;
    }
    public int Id;
    public string Name;
    public int Age;
    public string Email;
}


