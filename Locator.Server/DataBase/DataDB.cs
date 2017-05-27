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
        private static string dataDBfilename = "data.db";

     
        public static void addData(Data data)
        {
            using (var db = new LiteDatabase(dataDBfilename))
            {
                var datas = db.GetCollection<Data>("data");
                datas.Insert(data);
                Console.WriteLine(datas.Count());
            }
        }
    }
}
