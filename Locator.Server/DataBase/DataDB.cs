using LiteDB;
using Locator.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locator.Server.DataBase
{
    public static class DataDB
    {
        //For every user - single DB by ID
        public static void AddData(State state)
        {
            var fileName = state.UserID + ".db";
            using (var db = new LiteDatabase(fileName))
            {
                var datas = db.GetCollection<State>("data");
                datas.Insert(state);
                Console.WriteLine(datas.Count());
            }
        }

        public static State GetLastData(string UserID)
        {
            var fileName = UserID + ".db";
            using (var db = new LiteDatabase(fileName))
            {
                var datas = db.GetCollection<State>("data");
                var lastState = datas.Find(Query.All(Query.Descending), limit: 1).ToArray()[0];
                return lastState;
            }
        }
    }
}
