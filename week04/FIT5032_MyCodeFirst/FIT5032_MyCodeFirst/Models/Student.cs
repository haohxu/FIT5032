using System;
using System.Collections.Generic;

namespace FIT5032_MyCodeFirst.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    
        public virtual ICollection<Enrolment> Enrolments { get; set;  }
    }
}