using Domin.Models;
using Domin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.SectionServices
{
    public interface ISectionServices
    {
        public Task<ResultServices> AddSection(Section model);
        public Task<ResultServices> DeleteSection(Section model);
        public Task<ResultServices> UpdateSection(Section model);
        public Task<List<Section>> GetSections(int depid);
        public  Task<List<Section>> GetSections(int depid, int levelid);
        public Task<List<Student>> StudentsWithSection(int sectionid);
        public Task<List<ProfessorShowViewModel>> ProfessoresWithSection(int sectionid);
        public Task<ResultServices> AddProfessorInSection(TeachBy model);
        public  Task<List<Professor>> GetProfessorsWithDep(int depid);
        public Task<List<Course>> GetCoursesProfessorWithDep(int professorId, int departmentId, int levelid);
        public  Task<ResultServices> ValidationAddProfessorInSection(int Sectionid, int couresid);
    }
}
