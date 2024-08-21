using Domin.Models;
using Infrastructure.Services.ProfessorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.Controller
{
    public class ProfessorController
    {
        private readonly IProfessorServices _ProfessorSeveices;
        public ProfessorController(IProfessorServices professorServices)
        {
            _ProfessorSeveices = professorServices;
        }


        public async Task<bool> AddProfessor(Professor model)
        {
            if (model == null) return false;

            try
            {
                var result = await _ProfessorSeveices.AddProfessor(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }
          

        }
        public async Task<bool> UpdataProfessor(Professor model)
        {
            if (model == null) return false;

            try
            {
                var result = await _ProfessorSeveices.UpdateProfessor(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }


        }
        public async Task<bool> DeleteProfessor(Professor model)
        {
            if (model == null) return false;

            try
            {
                var result = await _ProfessorSeveices.DeleteProfessor(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }


        }

        public async Task<Professor>FindeById(int id)
        {
            return await _ProfessorSeveices.FindById(id);
        }

        public async Task<List<Professor>> GetProfessorsAsync()
        {
            return await _ProfessorSeveices.GetAllProfessor();
        }

        // GetStudentsWithProfessor
        public async Task<List<Student>> GetStudentsWithProfessor(int proid)
        {
            return await _ProfessorSeveices.GetStudentsWithProfessor(proid);
        }

        // GetCoursesWithProfessor
        public async Task<List<Course>> GetCoursesWithProfessor(int proid)
        {
            return await _ProfessorSeveices.GetCoursesWithProfessor(proid);
        }
        // GetDepartmentsWithProfessor
        public async Task<List<Department>> GetDepartmentsWithProfessor(int proid)
        {
            return await _ProfessorSeveices.GetDepartmentsWithProfessor(proid);
        }

        public async Task<bool> AddProfesserInDep(TeachIn model)
        {
            if (model == null) return false;

            try
            {
                var result = await _ProfessorSeveices.AddProfessorInDep(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }
        }

        public async Task<bool> AddCouresProfesser(TeachTo model)
        {
            if (model == null) return false;

            try
            {
                var result = await _ProfessorSeveices.AddCoursesProfessor(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }
        }

        public async Task<List<Course>> GetCoursesWithLevel(int idLevel)
        {
            return await _ProfessorSeveices.GetCoursesWithLevel(idLevel);
        }

    }
}
