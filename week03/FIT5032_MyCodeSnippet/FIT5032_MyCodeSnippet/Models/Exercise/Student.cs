using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_MyCodeSnippet.Models.Exercise
{
    public class Student
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Student(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }

    public class ExampleDictionary
    {
        public void Example()
        {
            Dictionary<int, Student> students = new Dictionary<int, Student>();

            Student s1 = new Student("Steve", "");
            Student s2 = new Student("James", "");

            students.Add(1, s1);
            students.Add(2, s2);

            Student result = new Student("", "");

            students.TryGetValue(1, out result);

        }
    }
}