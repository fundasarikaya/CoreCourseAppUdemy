using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Student
    {
        /*Student tablosu ile StudentAddress tablolaları arasında bire bir ilişki var yani bir ogrencinin bir adresi bir adresin bir ogrencisi var*/
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentAddress Address { get; set; } //datacontext e git ve dbset olarak ekle

        //ders 48 coka cok ilişki yapmak icin COURSE tablosuyla
        public IEnumerable<StudentCourse> StudentCourses { get; set; } //CMD DOTNET EF MİGRAİTONS ADD MANYTOMANY
        //dotnet ef database drop --force
        //dotnet ef database update
    }
}
