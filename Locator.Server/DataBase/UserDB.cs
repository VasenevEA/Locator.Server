using LiteDB;
using Locator.API;

namespace Locator.Server.DataBase
{
    public static class UserDB
    {
        private static string userDBfilename = "user.db";

        public static bool existUser(string email)
        {
            using (var db = new LiteDatabase(userDBfilename))
            {
                var users = db.GetCollection<User>("users");
                var user = users.FindOne(x => x.Email == email);

                return user != null;
            }
        }

        public static User getUser(string email,string password)
        {
            using (var db = new LiteDatabase(userDBfilename))
            {
                var users = db.GetCollection<User>("users");
                return users.FindOne(x => x.Email == email && x.Password == password);
            }
        }

        public static int getCount()
        {
            using (var db = new LiteDatabase(userDBfilename))
            {
                var users = db.GetCollection<User>("users");
                return users.Count();
            }
        }

        public static void addUser(User user)
        {
            using (var db = new LiteDatabase(userDBfilename))
            {
                var users = db.GetCollection<User>("users");
                users.Insert(user);
            }
        }

        public static void delUser(User user)
        {
            using (var db = new LiteDatabase(userDBfilename))
            {
                var users = db.GetCollection<User>("users");
                users.Delete(x => x.Email == user.Email);
            }
        }

    }
}
