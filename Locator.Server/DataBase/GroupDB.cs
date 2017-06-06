using LiteDB;
using Locator.API;
using Locator.Server.Models;
using System.Linq;

namespace Locator.Server.DataBase
{
    public static class GroupDB
    {
        private static string groupsDBfilename = "groups.db";
 
        public static Group[] GetAll()
        {
            using (var db = new LiteDatabase(groupsDBfilename))
            {
                var groups = db.GetCollection<Group>("groups");
                return groups.FindAll().ToArray<Group>();
            }

        }
        public static Group Get(int GroupID)
        {
            using (var db = new LiteDatabase(groupsDBfilename))
            {
                var groups = db.GetCollection<Group>("groups");
                return groups.FindOne(x => x.ID == GroupID);
            }
        }

        public static int Count()
        {
            using (var db = new LiteDatabase(groupsDBfilename))
            {
                var users = db.GetCollection<Group>("groups");
                return users.Count();
            }
        }

        public static void Add(Group group)
        {
            //Add in common file
            using (var db = new LiteDatabase(groupsDBfilename))
            {
                var groups = db.GetCollection<Group>("groups");
                groups.Insert(group);
            }
        }

        public static void Del(Group group)
        {
            using (var db = new LiteDatabase(groupsDBfilename))
            {
                var groups = db.GetCollection<Group>("groups");
                groups.Delete(x => x.ID == group.ID);
            }
        }

    }
}
