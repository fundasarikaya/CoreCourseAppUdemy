using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Request> Requests { get; set; }
        //Database oluşturmak icin projeye App Setting File ekle yada json dosyası
        //datacontext dosyasını statup.cs de tanıt
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; } //build et ve cmd ac dotnet ef migrations add oneToOneRelationships
        //daha sonra dotnet ef database update
    }
}
