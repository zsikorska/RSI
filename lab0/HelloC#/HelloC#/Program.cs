using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;

Console.Write("Podaj imię: ");
string imie = Console.ReadLine();
Console.WriteLine("Witaj " + imie);
Console.WriteLine(DateTime.Now.ToString("dd MMMM, HH:mm:ss", new CultureInfo("pl-PL")));
