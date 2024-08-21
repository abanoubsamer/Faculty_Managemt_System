using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ProfessorServices
{
    public interface IProfessorServices
    {

        public Task<ResultServices> AddProfessor(Professor model);
        public Task<ResultServices>  DeleteProfessor(Professor model);
        public Task<ResultServices> UpdateProfessor(Professor model);
        public  Task<List<Course>> GetCoursesWithLevel(int idLevel);

        public Task<List<Professor>> GetAllProfessor();
        public Task<Professor> FindById(int id);

        public Task<List<Student>> GetStudentsWithProfessor(int idProfessor);
        public Task<List<Course>> GetCoursesWithProfessor(int idProfessor);
        public Task<List<Department>> GetDepartmentsWithProfessor(int idProfessor);

        public Task<ResultServices> AddCoursesProfessor(TeachTo model);
        public Task<ResultServices> AddProfessorInDep(TeachIn model);






    }
}
