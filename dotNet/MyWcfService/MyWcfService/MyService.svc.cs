using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Text;

namespace MyWcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MyService : IMyService
    {
        public string GetNumber()
        {
            Console.WriteLine("GetNumber");

            return "6";
        }

        public string GetSampleMethod(string strUserName)
        {
            Console.WriteLine("GetSampleMethod: " + strUserName);

            StringBuilder strReturnValue = new StringBuilder();
            // return username prefixed as shown below
            strReturnValue.Append(string.Format
            ("You have entered userName as {0}", strUserName));
            return strReturnValue.ToString();
        }

        public List<Person> GetPersons()
        {
            Console.WriteLine("GetPersons");

            var persons = new List<Person>() {
                new Person(){Name = "Timmy", Age = 30},
                new Person(){Name = "Jimmy", Age = 32}
            };

            return persons;
        }

        public string PostSampleMethod(string data)
        {
            Console.WriteLine("PostSampleMethod: " + data);

            return "POSTED " + data;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}