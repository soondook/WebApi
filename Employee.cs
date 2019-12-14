using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    class Employee
    {
        public string FirstName = "";
        public string LastName = "";
        public int employeeID = 0;
        public string designation = "";

        public Employee(string firstName, string lastName, int EmployeeID, string Designation)
        {
            FirstName = firstName;
            LastName = lastName;
            employeeID = EmployeeID;
            designation = Designation;
        }

        public static string Json_pars(string FirstNames, string LastName, int EmployeeID, string Designation)
        {
            Employee emp = new Employee(FirstNames, LastName, EmployeeID, Designation);
            object JSONresult = JsonConvert.SerializeObject(emp);
            JSONDeserilaize(JSONresult);
            string path = "C:\\Temp\\employee.json.txt";
            using (TextWriter tw1 = new StreamWriter(path, true))
            {
                string JSresult = JSONresult.ToString();
                tw1.WriteLine(JSresult);
                tw1.Close();

            }
            Console.WriteLine(JSONresult);
            return JSONresult.ToString();
        }

        public static object JSONDeserilaize(object json)
        {
            Employee empObj = JsonConvert.DeserializeObject<Employee>(json.ToString());
            //Console.WriteLine(empObj.designation);
            return empObj.designation;

        }

    }
}
