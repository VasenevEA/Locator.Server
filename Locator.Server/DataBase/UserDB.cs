using LiteDB;
using Locator.API;
using System;
using System.Linq;

namespace Locator.Server.DataBase
{
    public static class UserDB
    {
        private static string usersDBfilename = "users.db";

        public static bool Exist(string email)
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {
                var users = db.GetCollection<User>("users");
                var user = users.FindOne(x => x.Email == email);

                return user != null;
            }
        }

        public static User[] GetAll()
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {
                var users = db.GetCollection<User>("users");
                return users.FindAll().ToArray<User>();
            }

        }
        public static User GetUser(string email,string password)
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {
     
                var users = db.GetCollection<User>("users");
                return users.FindOne(x => x.Email == email && x.Password == password);
            }
        }

        public static User GetUser(string token)
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {

                var users = db.GetCollection<User>("users");
                return users.FindOne(x => x.Token == token);
            }
        }

        public static int Count()
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {
                var users = db.GetCollection<User>("users");
                return users.Count();
            }
        }

        public static void AddUser(User user)
        {
            //Add in common file
            using (var db = new LiteDatabase(usersDBfilename))
            {
                var users = db.GetCollection<User>("users");
                users.Insert(user);
            }
        }

        public static void DelUser(User user)
        {
            using (var db = new LiteDatabase(usersDBfilename))
            {
                var users = db.GetCollection<User>("users");
                users.Delete(x => x.Email == user.Email);
            }
        }

    }
}
