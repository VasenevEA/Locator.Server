using Locator.Server.DataBase;
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
            while (true)
            {
                var command = Console.ReadLine();

                switch (command)
                {
                    case "users":
                        var users = UserDB.GetAll();
                        foreach (var user in users)
                        {
                            Console.WriteLine("ID: {0}  GroupID: {1}   Name: {2} {3}", user.ID, user.FirstName, user.LastName);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
