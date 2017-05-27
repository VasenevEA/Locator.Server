using Locator.API;
using Locator.Server.DataBase;
using Newtonsoft.Json;
using System;

namespace Locator.Server.Controllers
{
    public class DataController : Nancy.NancyModule
    {
        public DataController()
        {
            Get["/location"] = x =>
            {
                return "";
            };

            Post["/location"] = x =>
            {
                var body = this.Request.Body;
                int length = (int)body.Length;
                byte[] data = new byte[length];
                body.Read(data, 0, length);
                var text = System.Text.Encoding.Default.GetString(data);
                Console.WriteLine(text);
                try
                {
                    DataDB.addData(JsonConvert.DeserializeObject<Data>(text));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR Parse");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return "OK";
            };
        }
    }
}
