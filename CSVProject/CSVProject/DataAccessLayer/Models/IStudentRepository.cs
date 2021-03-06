using CSVProject.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVProject.DataAccessLayer.Models
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents(int csvFileId);

        Task<Student> GetStudent(int csvFileId, int studentNumber);

        Task<Student> GetStudentByFirstName(int csvFileId, string firstName);

        Task<Student> AddStudent(int csvFileId, Student student);

        Task<IEnumerable<Student>> AddStudents(int csvFileId, IEnumerable<Student> students);

        Task<Student> UpdateStudent(int csvFileId, Student student);

        Task DeleteStudent(int csvFileId, int studentNumber);

        Task<IEnumerable<Student>> Search(int csvFileId, string firstname, string surname, string courseCode, string courseDescription, string grade);

    }
}
