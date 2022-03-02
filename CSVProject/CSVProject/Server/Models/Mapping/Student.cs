using CsvHelper.Configuration.Attributes;

namespace CSVProject.Server.Models.Mapping
{
    public class Student
    {
        [Name("Student Number")]
        public int StudentNumber { get; set; }

        [Name("Firstname")]
        public string Firstname { get; set; }

        [Name("Surname")]
        public string Surname { get; set; }

        [Name("Course Code")]
        public string CourseCode { get; set; }

        [Name("Course Description")]
        public string CourseDescription { get; set; }

        [Name("Grade")]
        public string Grade { get; set; }
    }
}