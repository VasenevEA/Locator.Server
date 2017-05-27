using Locator.API;
using Nancy;
using System;
using Newtonsoft.Json;
using Locator.Server.DataBase;

namespace Locator.Server.Controllers
{
    public class AuthController : NancyModule
    {
        public AuthController()
        {
            Get["/check"] = x =>
            {
                Response response = new Response();
                response.StatusCode = HttpStatusCode.OK;
                return response;
            };

            Post["/signin"] = x =>
            {
                var auth = getObject<Auth>();
                if (UserDB.existUser(auth.Email))
                {
                    return JsonConvert.SerializeObject(UserDB.getUser(auth.Email,auth.Password));
                }
                else
                {
                    Response response = new Response();
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
            };

            Post["/signup"] = x =>
            {
                Response response = new Response();

                var auth = getObject<Auth>();
                //Check auth
                if (UserDB.existUser(auth.Email))
                {
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }
                else
                {
                    //Create new user
                    UserDB.addUser(new User
                    {
                        Email = auth.Email,
                        Name = auth.Name,
                        Password = auth.Password,
                        ID = UserDB.getCount() + 1 //ID auto generate ;)
                    });

                    return JsonConvert.SerializeObject(UserDB.getUser(auth.Email,auth.Password));
                }
            };
        }

        public T getObject<T>()
        {
            var body = this.Request.Body;
            int length = (int)body.Length;
            byte[] data = new byte[length];
            body.Read(data, 0, length);

            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.Default.GetString(data));
        }
    }
}
