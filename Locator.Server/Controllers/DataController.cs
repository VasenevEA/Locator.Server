using Locator.API;
using Locator.Server.DataBase;
using Nancy;
using Newtonsoft.Json;
using System;

namespace Locator.Server.Controllers
{
    public class DataController : Nancy.NancyModule
    {
        public DataController()
        {
            Get["/api/state"] = x =>
            {
                //Return state all group
                return "";
            };

            Post["/api/state"] = x =>
            {
                try
                {
                    var request = GetObject<Packet>();
                    //find userBy token
                    var user = UserDB.GetUser(request.Token);
                    //check if time no end
                    //decrypt 
                    var textData = Decrypt(request.Data, user.Key);
                    //
                    var state = JsonConvert.DeserializeObject<State>(textData);
                    DataDB.AddData(state);
                    Console.WriteLine(user.FirstName + " " + state.DateTime + " " + state.Longitude + " " + state.Latitude);

                    //TODO: Return state all group
                    //Find all users in group.
                    //Find State for every User
                    //Response
                    return "OK";
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR " + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                    return new Response().StatusCode = HttpStatusCode.NonAuthoritativeInformation;
                }
                
            };
        }

        public T GetObject<T>()
        {
            var body = this.Request.Body;
            int length = (int)body.Length;
            byte[] data = new byte[length];
            body.Read(data, 0, length);

            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.Default.GetString(data));
        }

        private string Encrypt(string value, string key)
        {
            //TEST
            Console.WriteLine("Encrypted the {0} by key {1} ", value, key);
            return value;
        }

        private string Decrypt(string value, string key)
        {
            //TEST
            Console.WriteLine("Decrypted the {0} by key {1} ", value, key);
            return value;
        }

        private class Packet
        {
            public string Token { get; set; }
            public string Data { get; set; }
        }
    }
}
