using CSVProject.Server.Constants;
using CSVProject.Server.Helpers;
using CSVProject.Server.Models.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProject.Server.Models
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ICsvRepository csvRepository;

        public StudentRepository(ICsvRepository csvRepository)
        {
            this.csvRepository = csvRepository;
        }

        public async Task<Student> AddStudent(int csvFileId, Student student)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                CsvFileHelper.UpdateFile<Student>($"{CsvConstants.Directory}{result.FileName}", new List<Student>() { student  });
            }
            return student;

        }

        public async Task<IEnumerable<Student>> AddStudents(int csvFileId, IEnumerable<Student> students)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                CsvFileHelper.UpdateFile<Student>($"{CsvConstants.Directory}{result.FileName}", students);
            }
            return students;
        }

        public async Task DeleteStudent(int csvFileId, int studentNumber)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                var students = CsvFileHelper.ReadFile<Student>($"{CsvConstants.Directory}{result.FileName}");
                var studentToDelete = students.FirstOrDefault(s => s.StudentNumber == studentNumber);
                if (studentToDelete != null)
                {
                    students = students.Where(s => s.StudentNumber != studentToDelete.StudentNumber);
                    CsvFileHelper.UpdateFile<Student>($"{CsvConstants.Directory}{result.FileName}", students, true);
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents(int csvFileId)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                return CsvFileHelper.ReadFile<Student>($"{CsvConstants.Directory}{result.FileName}");
            }
            return null;

        }

        public async Task<Student> GetStudent(int csvFileId, int studentNumber)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                return CsvFileHelper.ReadFile<Student>($"{CsvConstants.Directory}{result.FileName}").FirstOrDefault(s=>s.StudentNumber == studentNumber);
            }
            return null;
        }

        public async Task<Student> GetStudentByFirstName(int csvFileId, string firstName)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                return CsvFileHelper.ReadFile<Student>($"{CsvConstants.Directory}{result.FileName}").FirstOrDefault(s => s.Firstname == firstName);
            }
            return null;
        }

        public async Task<Student> UpdateStudent(int csvFileId, Student student)
        {
            var result = await csvRepository.GetCsv(csvFileId);

            if (result != null)
            {
                var students = CsvFileHelper.ReadFile<Student>($"{CsvConstants.Directory}{result.FileName}");
                var studentToUpdate = students.FirstOrDefault(s => s.StudentNumber == student.StudentNumber);
                if (studentToUpdate != null)
                {
                    studentToUpdate.Firstname = student.Firstname;
                    studentToUpdate.Surname = student.Surname;
                    studentToUpdate.CourseCode = student.CourseCode;
                    studentToUpdate.CourseDescription = student.CourseDescription;
                    studentToUpdate.Grade = student.Grade;

                    students = students.Where(s => s.StudentNumber != student.StudentNumber);
                    students.ToList().Add(studentToUpdate);

                    CsvFileHelper.UpdateFile<Student>($"{CsvConstants.Directory}{result.FileName}", students, true);
                    return studentToUpdate;
                }
            }
            return null;
        }
    }
}
