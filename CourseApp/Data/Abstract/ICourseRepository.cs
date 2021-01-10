using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public interface ICourseRepository
    {
       //IEnumerable<Request> Requests { get; }
        IQueryable<Course> Courses { get; }
        Course GetById(int courseid);
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCoursesByActive(bool isActive);
        IEnumerable<Course> GetCoursesByFilters(string name = null, decimal? price = null, string isActive=null);
        int CreateCourse(Course newCOurse);
        void UpdateCourse(Course updatedCourse,Course orginalCourse=null); //null dedik cunku herzaman gelmeyedebilir
        void DeleteCourse(int courseid);
        void UpdateAll(int id, Course[] courses);
    }
}
