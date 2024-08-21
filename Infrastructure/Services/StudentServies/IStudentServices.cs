using Domin.Models;
using Domin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.StudentServies
{
    public interface IStudentServices
    {
        public  Task<ResultServices>  AddStudent(Student model);
        public Task<ResultServices> UpdateStudent(Student model);
        public Task<ResultServices> DeleteStudent(Student model);
        public Task<Student>  FindById(int id);
        public Task<Student> FindName(string name);
        public Task<bool>  StudentExits(string name);
        public Task<List<Student>>  GetAllStudent();
        public Task<StudentDetailsDTO> GetStudentWithDetails(int studentid);
        public Task<Student> GetAllCouresStudent(int studentid);
        public Task<List<CourseProfessorDto>> GetAllProfessorStudent(int studentid);



    }
}
