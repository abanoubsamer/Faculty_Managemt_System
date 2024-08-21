using Domin.Models;
using Infrastructure.Services.DepartmentServices;
using Infrastructure.Services.LevelServices;
using Infrastructure.Services.StudentServies;
using Infrastructure.ValidateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.Controller
{
    public class LevelController
    {
        private readonly ILevelServices _LevelServices;
        public LevelController(ILevelServices levelServices)
        {
            _LevelServices = levelServices;
          }


        public async Task<bool> AddLevelController(Level? model)
        {
            var isValid = ModelStat.IsValid(model);
            if (isValid.Count == 0)
            {
                // If valid, add the student using the service
                var res = await _LevelServices.AddLevel(model);
                if (res.Succed)
                  return true;

                return false;
            }
            else
            {
                Console.WriteLine("Level data is not valid:");
                foreach (var result in isValid)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                return false;
            }

        }
        public async Task<Level> FindByIdController(int id)
        {
           
            if (id<=0) return null;

            return await _LevelServices.FindByID(id);
            
        }



        public async Task<List<Level>> GetLevels()
        {
            return await _LevelServices.GetLevels();
        }


        public async Task<List<Student>> GetStudentsWithLevel(int levelid)
        {
          return await _LevelServices.GetStudentsWithLevel(levelid);
        }
        public async Task<List<Course>> GetCoursesWithLevel(int levelid)
        {
            return await _LevelServices.GetCoursesWithLevel(levelid);
        }
        public async Task<List<Department>> GetDepartmentsWithLevel(int levelid)
        {
            return await _LevelServices.GetDepartmentsWithLevel(levelid);

        }

    }
}
