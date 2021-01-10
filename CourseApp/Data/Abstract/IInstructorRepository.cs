using CourseApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public interface IInstructorRepository:IGenericRepository<Instructor>
    {
        //Instructor Get(int id);
        //IEnumerable<Instructor> GetAll();
        //void Delete(int id);
        //void Update(Instructor entity);
        //void Insert(Instructor entity);
        //ders 41 de kapattım bu kısımları cunku artık generic bir repository yapısı oluşturduk bu yapı icerisindekiler hatic ozel bir sey yapmak icin burayı aşagıdaki degişitirdik ve aynı zamanda igenericrepositoryi buraya miras alarak implement yaptık
        IEnumerable<Instructor> GetTopInstructor();
    }
}
