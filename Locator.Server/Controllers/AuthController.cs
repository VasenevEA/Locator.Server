using Locator.API;
using Nancy;
using System;
using Newtonsoft.Json;
using Locator.Server.DataBase;
using System.Linq;

namespace Locator.Server.Controllers
{
    public class AuthController : NancyModule
    {
        public AuthController()
        {
            Get["/ping"] = x =>
            {
                return "Pong";
            };

            //Client send:      Email and Pass
            //Server response:  token and key, if Auth is ok
            Post["/api/login"] = x =>
            {
                var auth = GetObject<UserLoginModel>();
                if (UserDB.Exist(auth.Email))
                {
                    var user = UserDB.GetUser(auth.Email, auth.Password);
                    if (user.EndDT > DateTime.Now)
                    {
                        user.Token = GenerateToken();
                        user.Key = GenerateKey();
                        user.EndDT = DateTime.Now.AddDays(1);
                    }
                    return new {Token = user.Token, Key = user.Key };
                }
                else
                {
                    return new Response().StatusCode = HttpStatusCode.NotFound;
                }
            };

            //Client send:       Email, pass, FirstName, LastName, Pass
            //Server respons     ID, token, key
            Post["/api/reg"] = x =>
            {
                var reg = GetObject<UserRegModel>();

                if (!UserDB.Exist(reg.Email))
                {
                    //generate token and key for next auth
                    var user = new User
                    {
                        FirstName = reg.FirstName,
                        LastName = reg.LastName,

                        ID = UserDB.Count() + 1,
                        Email = reg.Email,
                        Password = reg.Password,
                        Token = GenerateToken(),
                        Key = GenerateKey(),
                        EndDT = DateTime.Now.AddDays(1)
                    };
                    UserDB.AddUser(user);
                    //Send response
                    return JsonConvert.SerializeObject(new {Token = user.Token, Key = user.Key });
                }
                else
                {
                    return new Response().StatusCode = HttpStatusCode.Conflict;
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

        private class UserLoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }

        private class UserRegModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private string GenerateKey()
        {
            string key = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            key = key.Replace("=", "");
            key = key.Replace("+", "");
            return key;
        }

        private string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            return token;
        }
    }
}
