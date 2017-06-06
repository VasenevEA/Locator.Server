using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locator.Server.Models
{
    public class Group
    {
        public int ID { get; set; }
        public int[] UsersID { get; set; }
    }
}
