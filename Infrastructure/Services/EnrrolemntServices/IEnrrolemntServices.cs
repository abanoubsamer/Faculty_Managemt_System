using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EnrrolemntServices
{
    public interface IEnrrolemntServices
    {

        public Task<ResultServices> AddEnrrolment(Enrollment model);
        public Task<ResultServices> UpdateEnrrolemnt(Enrollment model);
        public Task<ResultServices> DeleteEnrrolemnt(Enrollment Model);
        public Task<Enrollment> FindById(int id);
        public Task<List<Enrollment>> GetEnrollments();
        public Task<List<Enrollment>> GetEnrollmentsWithStudent(int studentid);
        public Task<List<Enrollment>> GetEnrollmentsWithCousrs(int courseid);
        public Task<List<Enrollment>> GetEnrollmentsWithLevel(int Levelid);
        public Task<List<Course>> GetCourseLeveAndDep(int stdid, int studentDep, int studentLevel);

    }
}
