using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services.StudentServies;
using Infrastructure.ValidateModel;
using Domin.Data;
using Domin.Models;
using System.Reflection.Metadata;
using Infrastructure.Services.LevelServices;
using Infrastructure.Services.DepartmentServices;
using Domin.Models.ViewModel;
using Infrastructure.Services.SectionServices;

namespace Program.Applicaction.Controller
{
    public class StudentController
    {

        private readonly IStudentServices _StudentServices;
        private readonly ILevelServices _LevelServices;
        private readonly IDepartmentServices _DepartmentServices;
        private readonly ISectionServices _SectionServices;
        public StudentController(IStudentServices studentServices,
            ILevelServices levelServices,
            IDepartmentServices departmentServices,
            ISectionServices SectionServices)
        {
            _DepartmentServices = departmentServices;
            _LevelServices = levelServices;
            _StudentServices = studentServices;
            _SectionServices = SectionServices;
        }
        public  async Task<bool> AddStudnetController(Student? model)
        {
            var isValid= ModelStat.IsValid(model);
            if (isValid.Count == 0)
            {
                // If valid, add the student using the service
                var res= await _StudentServices.AddStudent(model);
                if(res.Succed)
                return true;

                return false;
            }
            else
            {
                Console.WriteLine("Student data is not valid:");
                foreach (var result in isValid)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                return false;
            }

        }

        public  async Task<List<Student>>  GetStudent()
        {
           return await  _StudentServices.GetAllStudent();

        }

        public  async Task<Student>  GetStudnetByID(int id)
        {
            try
            {
                var studnet = await _StudentServices.FindById(id);
                if(studnet == null)
                {
                    return null;
                }
                return studnet;


            }
            catch (Exception ex) {
                return null;
            }
          

        }
        
        
        public async Task<bool>  UpdateStudent(Student student)
        {
            try
            {
             var res=  await _StudentServices.UpdateStudent(student);
                    return res.Succed;
               
            }
            catch (Exception ex)
            {
                return false;

            }
            
        }


        public async Task<bool> DeleteStudent(Student student)
        {
            try
            {
                var res = await _StudentServices.DeleteStudent(student);
                return res.Succed;

            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public async Task<List<Level>>  GetLevels()
        {
            return await _LevelServices.GetLevels();
        }
        public async Task<List<Department>>  GetDep()
        {
            return await _DepartmentServices.GetDepartments();
        }

        public async Task<Student> GetStudnetCourses(int id)
        {
            try
            {
                var studnet = await _StudentServices.GetAllCouresStudent(id);
                if (studnet == null)
                {
                    return null;
                }
                return studnet;


            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<List<CourseProfessorDto>> GetStudnetProfessors(int id)
        {
            try
            {
                var studnet = await _StudentServices.GetAllProfessorStudent(id);
                if (studnet == null)
                {
                    return null;
                }
                return studnet;


            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<StudentDetailsDTO> GetStudnetDetalis(int id)
        {
            try
            {
                var studnet = await _StudentServices.GetStudentWithDetails(id);
                if (studnet == null)
                {
                    return null;
                }
                return studnet;


            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<List<Section>> GetSection(int depid)
        {
            return await _SectionServices.GetSections(depid);
        }

    }

}
