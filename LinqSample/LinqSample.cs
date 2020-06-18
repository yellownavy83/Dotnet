using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    class LinqSample
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee("Jin", "Park", 100, true));
            employees.Add(new Employee("Abc", "Park", 1000, false));
            employees.Add(new Employee("Jin", "Kim", 100, true));
            employees.Add(new Employee("Abc", "Kim", 1000, false));

            List<Employee> managers = new List<Employee>();
            foreach(Employee employee in employees)
            {
                if(employee.IsManager == true)
                {
                    managers.Add(employee);
                    Console.WriteLine(employee);
                }
            }

            Console.WriteLine("\n=============Linq FindAll() Test=============");
            List<Employee> managers2 = employees.FindAll(employee => employee.IsManager == true);
            managers2.ForEach(e => Console.WriteLine(e));

            Console.WriteLine("\n=============Linq OrderBy() Test=============");
            List<Employee> sortedList = employees.OrderBy(e => e.Salary).ToList();
            sortedList.ForEach(e => Console.WriteLine(e));

            Console.WriteLine("\n=============Linq OrderByDescending() Test=============");
            List<Employee> sortedList2 = employees.OrderByDescending(e => e.Salary).ToList();
            sortedList2.ForEach(e => Console.WriteLine(e));

            Console.WriteLine("\n=============Linq OrderBy() ThenBy() Test=============");
            //List<Employee> sortedList3 = employees.OrderBy(e => e.Salary).ThenBy(o => o.LastName).ToList();
            List<Employee> sortedList3 = employees.OrderBy(e => e.Salary).ThenByDescending(o => o.LastName).ToList();
            sortedList3.ForEach(e => Console.WriteLine(e));

            Console.WriteLine("\n=============Linq Sort() Test=============");
            employees.Sort((o1, o2) => o1.LastName.CompareTo(o2.LastName));
            employees.ForEach(e => Console.WriteLine(e));

            Console.Read();
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public bool IsManager { get; set; }
        public Employee(string FirstName, string LastName, decimal Salary, bool IsManager)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Salary = Salary;
            this.IsManager = IsManager;
        }
        public override string ToString()
        {
            return "FirstName: " + FirstName + " LastName: " + LastName + " Salary: " + Salary + " IsManager: " + IsManager;
        }
    }
}
