using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FIT5032_MyCodeFirst.DAL;
using FIT5032_MyCodeFirst.Models;
using System.Diagnostics;

namespace FIT5032_MyCodeFirst.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
            new Student{FirstName="Carson",LastName="Alexander",DOB=DateTime.Parse("2000-01-02")},
            new Student{FirstName="Meredith",LastName="Alonso",DOB=DateTime.Parse("1997-03-04")},
            new Student{FirstName="Arturo",LastName="Anand",DOB=DateTime.Parse("1998-05-06")},
            new Student{FirstName="Gytis",LastName="Barzdukas",DOB=DateTime.Parse("1997-07-08")},
            new Student{FirstName="Yan",LastName="Li",DOB=DateTime.Parse("1997-09-11")},
            new Student{FirstName="Peggy",LastName="Justice",DOB=DateTime.Parse("1996-10-22")},
            new Student{FirstName="Laura",LastName="Norman",DOB=DateTime.Parse("1998-11-11")},
            new Student{FirstName="Nino",LastName="Olivetto",DOB=DateTime.Parse("2000-12-12")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var Enrolments = new List<Enrolment>
            {
                new Enrolment{StudentID=1,CourseID=1050},
                new Enrolment{StudentID=1,CourseID=4022},
                new Enrolment{StudentID=1,CourseID=4041},
                new Enrolment{StudentID=2,CourseID=1045},
                new Enrolment{StudentID=2,CourseID=3141},
                new Enrolment{StudentID=2,CourseID=2021},
                new Enrolment{StudentID=3,CourseID=1050},
                new Enrolment{StudentID=4,CourseID=1050},
                new Enrolment{StudentID=4,CourseID=4022},
                new Enrolment{StudentID=5,CourseID=4041},
                new Enrolment{StudentID=6,CourseID=1045},
                new Enrolment{StudentID=7,CourseID=3141},
            };
            Enrolments.ForEach(s => context.Enrolments.Add(s));
            context.SaveChanges();
        }
    }
}