using Locator.API;
using Locator.Server.DataBase;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locator.Server.Controllers
{
    public class DataController : Nancy.NancyModule
    {
        public DataController()
        {
            Post["/api/group/all", true] = async (x, ct) =>
            {
                await Task.Delay(100);
                var request = GetObject<Packet>();
                //find userBy token
                var key = UserDB.GetUser(request.Token).Key;

                //for test return last state of all users
                var groupState = new List<State>();
                var users = UserDB.GetAll();
                foreach (var user in users)
                {
                    var state = DataDB.GetLastData(user.ID);
                    if (state != null)
                    {
                        groupState.Add(state);
                    }
                }

                var packet = new Packet
                {
                    Token = null,
                    Data = Encrypt(JsonConvert.SerializeObject(groupState), key)
                };
                return JsonConvert.SerializeObject(packet);
            };

            Get["/api/group/join"] = x =>
            {

                return "";
            };

            Post["/api/state",true] = async (x, ct) =>
            {
                await Task.Delay(100);
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
                    Console.WriteLine(user.ID + " " + user.FirstName + " " + state.DateTime + " " + state.Longitude + " " + state.Latitude);

                    return "ok";
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
           // Console.WriteLine("Encrypted the {0} by key {1} ", value, key);
            return value;
        }

        private string Decrypt(string value, string key)
        {
            //TEST
           // Console.WriteLine("Decrypted the {0} by key {1} ", value, key);
            return value;
        }

        private class Packet
        {
            public string Token { get; set; }
            public string Data { get; set; }
        }
    }
}
