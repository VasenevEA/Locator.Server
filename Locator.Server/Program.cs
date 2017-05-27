using Nancy.Hosting.Self;
using System;

namespace Locator.Server
{
    class Program
    {
   
        static void Main(string[] args)
        {
            var conf = Service.readConfig();

            NancyHost host = new NancyHost(new System.Uri(conf.url));
            host.Start();

            Console.WriteLine("Server starting on {0}", conf.url);
            Console.ReadKey();
        }
    }
}
