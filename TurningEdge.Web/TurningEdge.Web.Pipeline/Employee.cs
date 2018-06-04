using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Web.Pipeline
{
    public class Employee
    {
        private int _age;
        private double _salary;
        private string _name;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public double Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Employee(int age, double salary, string name)
        {
            _age = age;
            _salary = salary;
            _name = name;
        }

        public override string ToString()
        {
            return "Employee{_age: " + _age + ", " +
                "_salary: " + _salary + ", " +
                "_name: " + _name + "}";
        }
    }
}
