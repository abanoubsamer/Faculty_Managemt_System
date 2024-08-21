using Domin.Models;
using Infrastructure.Services;
using Infrastructure.Services.CouresServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.Controller
{
    public class CouresController
    {
        private readonly ICouresServices _couresServices;

        public CouresController(ICouresServices couresServices)
        {
            _couresServices = couresServices;
        }


        public async Task<bool> AddCoures(Course model)
        {
            var result = new ResultServices();
            try
            {
               result= await _couresServices.AddCoures(model);
               return result.Succed;
            }
            catch
            {
                Console.WriteLine($"{result.MsgError}");
                return false;
            }
        }
        public async Task<bool> UpdateCoures(Course model)
        {
            var result = new ResultServices();
            try
            {
                result = await _couresServices.UpdateCoures(model);
                return result.Succed;
            }
            catch
            {
                Console.WriteLine($"{result.MsgError}");
                return false;
            }
        }
        public async Task<bool> DeleteCoures(Course model)
        {
            var result = new ResultServices();
            try
            {
                result = await _couresServices.DeleteCoures(model);
                return result.Succed;
            }
            catch
            {
                Console.WriteLine($"{result.MsgError}");
                return false;
            }
        }
        public async Task<bool> AddCouresInDepartemnt(CourseDepartment model)
        {
            var result = new ResultServices();
            try
            {
                result = await _couresServices.AddCouresInDepartemnt(model);
                return result.Succed;
            }
            catch
            {
                Console.WriteLine($"{result.MsgError}");
                return false;
            }
        }
        public async Task<Course> FindById(int id)
        {
            if (id > 0)
            {
                return await _couresServices.FindById(id);
            }
            else
            {
                Console.WriteLine("Invalid Id");
                return null;
            }
        }
        public async Task<List<Course>> GetCourses()
        {
            return await _couresServices.GetCourses();
        }
        public async Task<List<Course>> GetCourses(int levelid)
        {
            if(levelid>0)
            return await _couresServices.GetCourses(levelid);
            return null;
        }
        public async Task<List<Course>> GetCourses(int levelid, int depid)
        {
            if(levelid>0&&depid>0)
            return await _couresServices.GetCourses(levelid, depid);
            return null;
        }

    }
}
