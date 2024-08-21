using Domin.Models;
using Infrastructure.Services.EnrrolemntServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Applicaction.Controller
{
    public class EnrrolemntController
    {
        private readonly IEnrrolemntServices _enrrolemntServices;
        public EnrrolemntController(IEnrrolemntServices enrrolemntServices)
        {
            _enrrolemntServices = enrrolemntServices;
        }

        public async Task<bool> AddEnrollments(Enrollment model)
        {
            if (model == null) return false;

            try
            {
                var result = await _enrrolemntServices.AddEnrrolment(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }

        }

        public async Task<bool> UpdateEnrollments(Enrollment model)
        {
            if (model == null) return false;

            try
            {
                var result = await _enrrolemntServices.UpdateEnrrolemnt(model);
                return result.Succed;
            }
            catch
            {
                return false;

            }

        }
        public async Task<Enrollment> FindById(int id)
        {
            if (id <= 0) return null;

            return await _enrrolemntServices.FindById(id);

        }

        public async Task<List<Course>> GetCourseLeveAndDep(int stdid, int stdLevel,int stdDep)
        {
            return await _enrrolemntServices.GetCourseLeveAndDep(stdid,stdDep,stdLevel);
        }
        public async Task<List<Enrollment>> GetEnrollments()
        {
            return await _enrrolemntServices.GetEnrollments();
        }
        public async Task<List<Enrollment>> GetEnrollmentswithLevel(int levelid)
        {
            return await _enrrolemntServices.GetEnrollmentsWithLevel(levelid);
        }
        public async Task<List<Enrollment>> GetEnrollmentswithCourses(int courseid)
        {
            
            return await _enrrolemntServices.GetEnrollmentsWithCousrs(courseid);

        }
        public async Task<List<Enrollment>> GetEnrollmentswithStudent(int studentid)
        {
            return await _enrrolemntServices.GetEnrollmentsWithStudent(studentid);
        }


    }
}
