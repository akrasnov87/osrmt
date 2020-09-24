using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmrt.Model
{
    public class ConnectionString
    {
        public string host { get; set; }
        public string port { get; set; }
        public string dbName { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return host + ":" + port;
        }
    }
}
