using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class EfCourseRepository : ICourseRepository
    {
        private DataContext context;
        public EfCourseRepository(DataContext _context)
        {
            context = _context;
        }
        //  public IEnumerable<Request> Requests => context.Requests;

        public IQueryable<Course> Courses => context.Courses; //burada extra sorguda gonderebilirz
        //public IEnumerable<Course> Courses => context.Courses; verileri bize getirken sartımızı uygulayıp oyle getirsin diye iquerable yap
        //ne zaman bizden irepository istenirse bizim o zaman efrepository i gondermemiz gerekir onun icin startup service ayarı eklenir

        public int CreateCourse(Course newCourse)
        {
            context.Courses.Add(newCourse);
            context.SaveChanges();
            return newCourse.Id;  //eklenen verinin id almaya calışıyyoruz
        }

        public void DeleteCourse(int courseid)
        {
            //  var entity = context.Courses.Find(courseid); //tekrardan bir sorgu yapmasın diye remove icerisine new course işlemi yapılıt
            //  context.Courses.Remove(entity);
            //context.Courses.Remove(new Course() {Id=courseid }); relation yapmadan once bu halde idi

            var entity = GetById(courseid);
            context.Courses.Remove(entity);
            if(entity.Instructor!=null)
            {
                context.Remove(entity.Instructor);
            }
            context.SaveChanges();
        }

        public Course GetById(int courseid)
        {
            // return context.Courses.Where(i => i.Id == courseid).FirstOrDefault();
            //return context.Courses
            //    .Include(i => i.Instructor)
            //    .FirstOrDefault(i => i.Id == courseid); //ders 38 oncesi contact ve adress tablolaru yokken
            return context.Courses
                .Include(i => i.Instructor)
                .ThenInclude(i=>i.Contact)
                .ThenInclude(i=>i.Address)
                .FirstOrDefault(i => i.Id == courseid);
        }

        public IEnumerable<Course> GetCourses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesByActive(bool isActive)
        {
            return context.Courses.Where(i => i.isActive == isActive).ToList(); //tolist yaptıgım icin ienumerable veya list olması onemli degil sorgu bi kere calışır
        }

        public IEnumerable<Course> GetCoursesByFilters(string name = null, decimal? price = null, string isActive = null)
        {
            IQueryable<Course> query = context.Courses;
            if(name!=null)
            {
                query = query.Where(i => i.Name.ToLower().Contains(name.ToLower()));
            }
            if(price!=null)
            {
                query = query.Where(i => i.Price >= price);
            }
            if(isActive=="on")
            {
                query = query.Where(i => i.isActive == true);
            }
            return query.Include(i=>i.Instructor).ToList();
            //return query.ToList();  //instructr ile foreign kurduktan sonra instructor verileirni de cshtml de cagırmak icin foreach kısmında instructor cagırınca iki sorgu yapar lazy loading kullandıgımız icin(virtual) tek sorgu olsun diye instructor'ı include ederiz

        }

        public void UpdateAll(int id, Course[] courses)
        {
            context.Courses.UpdateRange(courses.Where(i => i.InstructorId != id));
            context.SaveChanges();
        }
        #region UpdateCourse
        public void UpdateCourse(Course updatedCourse, Course orginalCourse = null)
        {
            //context.Courses.Update(updatedCourse);
            //context.SaveChanges();    Bu şekilde yaparsan formdaki tum elemanları gunceller 
            if (orginalCourse == null)
            {
                orginalCourse = context.Courses.Find(updatedCourse.Id);
            }
            else
            {
                context.Courses.Attach(orginalCourse);//change tracking yapar
            }
            //  var entity = context.Courses.Find(updatedCourse.Id); //bu işlemi yapmamak icin edit.cshtml sayfasında input hidden ile id name gibi alanları tutalabilirz cunku burası ekstra sorgu yapar

            orginalCourse.Name = updatedCourse.Name;
            orginalCourse.Description = updatedCourse.Description;
            orginalCourse.Price = updatedCourse.Price;
            orginalCourse.isActive = updatedCourse.isActive;

            orginalCourse.Instructor.Name = updatedCourse.Instructor.Name;
            //orginalCourse.Instructor.City = updatedCourse.Instructor.City;
            //change trackingleri goruntulemek icin
            EntityEntry entry = context.Entry(orginalCourse);
            //modified,unchanged,added,deleted,detached
            Console.WriteLine($"Entity State: { entry.State}");
            foreach (var property in new string[] { "Name", "Description", "Price", "isActive" })
            {
                Console.WriteLine($"{property} - old: {entry.OriginalValues[property]} new : {entry.CurrentValues[property]}");
            }
            context.SaveChanges();   //bu şekilde neresi degiştirildiyese orasını gunceller buna change tracking denir


        }
        #endregion   
    }
}
