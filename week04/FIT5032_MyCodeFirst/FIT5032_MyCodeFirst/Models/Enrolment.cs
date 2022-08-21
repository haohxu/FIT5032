using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FIT5032_MyCodeFirst.Models
{
    public class Enrolment
    {
        public int EnrolmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
   

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}