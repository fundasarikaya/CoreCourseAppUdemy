using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal  Price { get; set; }
        public bool isActive { get; set; }
        //bu hali ile instructor boşta olabilri 
        public Instructor Instructor { get; set; } //virtual lazy loading yapar
        public int InstructorId { get; set; } //Bu şekilde foreign kurarsak instrtuor boş gecilemez demektir public Instructor Instructor { get; set; } ile birlikte kullanıldıgında

        //ders 48 coka cok ilişki yapmak icin student tablosuyla
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
