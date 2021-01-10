using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string City { get; set; }

        //ders38
        public Contact Contact { get; set; }
        //ders41 instructor tablosundaki egitmenlerin hangi kursları veriklerini cekmek icin
        public IEnumerable<Course> Courses { get; set; }
    }
}
