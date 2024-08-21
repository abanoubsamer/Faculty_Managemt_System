using Domin.Models;
using Domin.Models.ViewModel;
using Infrastructure.Services;
using Infrastructure.Services.LevelServices;
using Infrastructure.Services.SectionServices;
using Program.Applicaction.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Program.Applicaction.Controller
{
    public class SectionController
    {
        private readonly ISectionServices _SectionServices;
        public SectionController(ISectionServices SectionServices)
        {
            _SectionServices = SectionServices;
        }


        public async Task<bool> AddSection(Domin.Models.Section model)
        {

            try
            {
                var res= await _SectionServices.AddSection(model);
                return res.Succed;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<bool> DeleteSection(Domin.Models.Section model)
        {

            try
            {
                var res = await _SectionServices.DeleteSection(model);
                return res.Succed;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateSection(Domin.Models.Section model)
        {

            try
            {
                var res = await _SectionServices.UpdateSection(model);
                return res.Succed;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Domin.Models.Section>> GetSection(int depid)
        {
            if (depid <= 0) return null;
            return await _SectionServices.GetSections(depid);
        }
        public async Task<List<Domin.Models.Section>> GetSection(int depid,int levelid)
        {
            if (depid <= 0) return null;
            return await _SectionServices.GetSections(depid,levelid);
        }
        public async Task<List<ProfessorShowViewModel>> GetProfessorsWithSection(int Sectionid)
        {
            if (Sectionid <= 0) return null;
            return await _SectionServices.ProfessoresWithSection(Sectionid);
        }
        public async Task<List<Student>> GetStudentWithSection(int stdid)
        {
            if (stdid <= 0) return null;
            return await _SectionServices.StudentsWithSection(stdid);
        }
        public async Task<List<Professor>> GetProfessorsWithDep(int depid)
        {
            return await _SectionServices.GetProfessorsWithDep(depid);
        }
        public async Task<bool> AddProfessorInSection(TeachBy model)
        {
            try
            {
                var validation = await _SectionServices.ValidationAddProfessorInSection(model.SectionId, model.CourseId);
                if (validation.Succed)
                {
                    var res = await _SectionServices.AddProfessorInSection(model);
                    return res.Succed;
                }
                else
                {
                    Layout.ShowMessageLine($"{validation.MsgError}",ConsoleColor.Red);
                    Console.ReadKey();
                    return false;
              
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Course>> GetCoursesProfessorWithDep(int professorid, int depid,int levelid)
        {
          

            return await _SectionServices.GetCoursesProfessorWithDep(professorid, depid, levelid);
        }
    }
}
