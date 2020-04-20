using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    class EmployeeIP
    {
        public string CompassName { get; set; }
        public string IpAddress { get; set; }
        public string Peer { get; set; }
        public int ID { get; set; }

            

        public static object JSONDeserilaize(object json)
        {
            EmployeeIP empObj = JsonConvert.DeserializeObject<EmployeeIP>(json.ToString());
            Console.WriteLine("JsonConvert");
            Console.WriteLine(empObj.Peer);
            return empObj;

        }

    }
}