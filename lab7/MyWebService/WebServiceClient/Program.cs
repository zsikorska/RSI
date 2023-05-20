using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
class Client
{
    static string uri = "http://localhost:2119/MyRestService.svc";

    static string format = "json";
    static void Main(string[] args)
    {
        MyData.Info();
        Menu();
    }

    static string countPersonsJson()
    {
        return getFromEndpoint("/json/persons/size");
    }

    static string countPersonsXml()
    {
        return getFromEndpoint("/persons/size");
    }

    static string allPersonsJson()
    {
        return getFromEndpoint("/json/persons");
    }

    static string allPersonsXml()
    {
        return getFromEndpoint("/persons");
    }

    static string getPersonByIdJson(int id)
    {
        return getFromEndpoint("/json/persons/"+id);
    }

    static string getPersonByIdXml(int id)
    {
        return getFromEndpoint("/persons/" + id);
    }

    static string getPersonByNameJson(string name)
    {
        return getFromEndpoint("/json/persons/name/"+name);
    }

    static string getPersonByNameXml(string name)
    {
        return getFromEndpoint("/persons/name/" + name);
    }

    static string getFromEndpoint(string endpoint)
    {
        HttpClient client = new HttpClient();
        var response = client.GetAsync(uri + endpoint).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string addNewPersonJson(string name, int age, string email)
    {
        string endpoint = "/json/persons";
        HttpClient client = new HttpClient();
        StringContent content = new StringContent(JsonConvert.SerializeObject(new Person(name, age, email)), Encoding.UTF8, "application/json");
        var response = client.PostAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string addNewPersonXml(string name, int age, string email)
    {
        string endpoint = "/persons";
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        string xmlPayload = $@"<Person xmlns=""http://schemas.datacontract.org/2004/07/MyWebService""><Name>{name}</Name><Age>{age}</Age><Email>{email}</Email></Person>";

        StringContent content = new StringContent(xmlPayload, Encoding.UTF8, "application/xml");
        var response = client.PostAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string editPersonJson(int id, string name, int age, string email)
    {
        string endpoint = "/json/persons";
        HttpClient client = new HttpClient();
        StringContent content = new StringContent(JsonConvert.SerializeObject(new Person(id, name, age, email)), Encoding.UTF8, "application/json");
        var response = client.PutAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string editPersonXml(int id, string name, int age, string email)
    {
        string endpoint = "/persons";
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        string xmlPayload = $@"<Person xmlns=""http://schemas.datacontract.org/2004/07/MyWebService""><Id>{id}</Id><Name>{name}</Name><Age>{age}</Age><Email>{email}</Email></Person>";

        StringContent content = new StringContent(xmlPayload, Encoding.UTF8, "application/xml");
        var response = client.PutAsync(uri + endpoint, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
    static string deletePersonJson(int id)
    {
        string endpoint = "/json/persons/";
        HttpClient client = new HttpClient();
        var response = client.DeleteAsync(uri + endpoint+id).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static string deletePersonXml(int id)
    {
        string endpoint = "/persons/";
        HttpClient client = new HttpClient();
        var response = client.DeleteAsync(uri + endpoint + id).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    static void Menu()
    {
        int num = 0;
        while (num != 9)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1) Zwróć liczbę osób");
            Console.WriteLine("2) Zwróć wszystkie osoby");
            Console.WriteLine("3) Zwróć osobę po id");
            Console.WriteLine("4) Dodaj osobę");
            Console.WriteLine("5) Zaktualizuj osobę");
            Console.WriteLine("6) Usuń osobę");
            Console.WriteLine("7) Filtruj osoby po imieniu");
            Console.WriteLine("8) Zmień format (aktualny: {0})", format);
            Console.WriteLine("9) Wyjście \n");
            Console.Write("Twój wybór: ");
            num = InputNumber();
            Console.WriteLine();

            if (num == 1)
            {
                if(format == "json")
                    Console.WriteLine($"Liczba osób: {countPersonsJson()}");
                else
                    Console.WriteLine($"Liczba osób: {countPersonsXml()}");

                Console.WriteLine();
            }
            else if (num == 2)
            {
                if(format == "json")
                    Console.WriteLine($"Lista wszystkich osób: \n{allPersonsJson()}\n");
                else
                    Console.WriteLine($"Lista wszystkich osób: \n{allPersonsXml()}\n");
            }
            else if (num == 3)
            {
                Console.Write("Podaj id: ");
                var id = InputNumber();
                if(format == "json")
                    Console.WriteLine($"Osoba o podanym id: \n{getPersonByIdJson(id)}\n");
                else
                    Console.WriteLine($"Osoba o podanym id: \n{getPersonByIdXml(id)}\n");
            }
            else if (num == 4)
            {
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();
                Console.Write("Podaj wiek: ");
                var age = InputNumber();
                Console.Write("Podaj email: ");
                var email = Console.ReadLine();

                if(format == "json")
                    Console.WriteLine($"\n{addNewPersonJson(name, age, email)}\n");
                else
                    Console.WriteLine($"\n{addNewPersonXml(name, age, email)}\n");
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

                if(format == "json")
                    Console.WriteLine($"\n{editPersonJson(id, name, age, email)}\n");
                else
                    Console.WriteLine($"\n{editPersonXml(id, name, age, email)}\n");
            }
            else if (num == 6)
            {
                Console.Write("Podaj id do usunięcia: ");
                var id = InputNumber();

                if(format == "json")
                    Console.WriteLine($"Osoba o podanym id: \n{deletePersonJson(id)}\n");
                else
                    Console.WriteLine($"Osoba o podanym id: \n{deletePersonXml(id)}\n");
            }
            else if (num == 7)
            {
                Console.Write("Podaj imię: ");
                var name = Console.ReadLine();

                if(format == "json")
                    Console.WriteLine($"Osoba o podanym id: \n{getPersonByNameJson(name)}\n");
                else
                    Console.WriteLine($"Osoba o podanym id: \n{getPersonByNameXml(name)}\n");
            }
            else if (num == 8)
            {
                if (format == "json")
                {
                    format = "xml";
                    Console.WriteLine("Zmieniono format na xml");
                    Console.WriteLine();
                }
                else
                {
                    format = "json";
                    Console.WriteLine("Zmieniono format na json");
                    Console.WriteLine();
                }
            }
            else if (num == 9)
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
    public Person(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
    }
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


