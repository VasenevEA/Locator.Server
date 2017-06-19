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
            Console.WriteLine(UserDB.Count());
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
                            Console.WriteLine("User ID: {0}  GroupID: {1}, Email: {2},  First Name: {3}  Last Name: {4}" , user.ID, user.GroupID , user.Email ,user.FirstName, user.LastName);
                        }
                        break;
                    case "groups":
                        var groups = GroupDB.GetAll();
                        foreach (var group in groups)
                        {
                            Console.WriteLine("GroupID: {0}", group.ID);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
