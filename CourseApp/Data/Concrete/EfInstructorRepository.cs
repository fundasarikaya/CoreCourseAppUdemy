using CourseApp.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class EfInstructorRepository :GenericRepository<Instructor>, IInstructorRepository
    {
        private DataContext _context;
        public EfInstructorRepository(DataContext context):base(context) //base kısmını ders 41 de ekledik //genericrepository ctor kısmına context bekledigi icin ekledik
        {
            _context = context; //dependency injection
        }


        //public void Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Instructor Get(int id)
        //{
        //    return _context.Instructors.Find(id);
        //}

        //public IEnumerable<Instructor> GetAll()
        //{
        //    return _context.Instructors;
        //}

        //public void Insert(Instructor entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Instructor entity)
        //{
        //    throw new NotImplementedException();
        //} //burayı neden yorum satırına aldıgımın acıklaması IInstructorrepository de var
        //miras kısmına Genereicrepository i ekledik ve IInstructorRepository implement ettik
        public IEnumerable<Instructor> GetTopInstructor()
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<Instructor> GetAll()
        {
            //  return _context.Instructors.Include(i=>i.Courses); //bu filtresiz kısım
            //filtre
            // _context.Courses.Where(i => i.Instructor != null && i.isActive).Load();
            _context.Courses.Where(i => i.Instructor != null).Load();
            return _context.Instructors;
        }
    }
}
