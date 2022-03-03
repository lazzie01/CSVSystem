using CSVProject.Server.Models.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProject.Server.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public int StudentNumber { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [Required]
        public string CourseDescription { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public int CsvId { get; set; }

        public StudentViewModel() { }

        public StudentViewModel(Student student) 
        {
            StudentNumber = student.StudentNumber;
            Firstname = student.Firstname;
            Surname = student.Surname;
            CourseCode = student.CourseCode;
            CourseDescription = student.CourseDescription;
            Grade = student.Grade;
        }

        public Student ToModel()
        {
            return new Student
            {
                StudentNumber = StudentNumber,
                Firstname = Firstname,
                Surname = Surname,
                CourseCode = CourseCode,
                CourseDescription = CourseDescription,
                Grade = Grade
            };
        }

    }
}
