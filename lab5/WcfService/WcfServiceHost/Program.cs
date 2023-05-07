using System;
using System.ServiceModel.Description;
using System.ServiceModel;
using WcfService;

namespace WcfServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            MyData.Info();

            Uri baseAddress = new Uri("http://localhost:5000/WcfService");
            ServiceHost myHost = new ServiceHost(typeof(MyCalculator), baseAddress);
            
            BasicHttpBinding myBinding = new BasicHttpBinding();
            ServiceEndpoint endpoint1 = myHost.AddServiceEndpoint(typeof(ICalculator), myBinding, "endpoint1");

            WSHttpBinding binding2 = new WSHttpBinding();
            binding2.Security.Mode = SecurityMode.None;
            ServiceEndpoint endpoint2 = myHost.AddServiceEndpoint(typeof(ICalculator), binding2, "endpoint2");

            Console.WriteLine("\n---> Endpoints:");
            Console.WriteLine("\nService endpoint {0}:", endpoint1.Name);
            Console.WriteLine("Binding: {0}", endpoint1.Binding.ToString());
            Console.WriteLine("ListenUri: {0}", endpoint1.ListenUri.ToString());

            Console.WriteLine("\nService endpoint {0}:", endpoint2.Name);
            Console.WriteLine("Binding: {0}", endpoint2.Binding.ToString());
            Console.WriteLine("ListenUri: {0}", endpoint2.ListenUri.ToString());

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            myHost.Description.Behaviors.Add(smb);

            try
            {
                myHost.Open();
                Console.WriteLine();
                Console.WriteLine("Serwis wystartował i działa");

                Console.WriteLine("Wciśnij <ENTER> aby zatrzymać serwis");
                Console.WriteLine();
                Console.ReadLine();
                myHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Wystapił wyjątek: {0}", ce.Message);
                myHost.Abort();
            }
        }
    }
}
