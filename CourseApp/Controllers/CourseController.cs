using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepository repository;
        public CourseController(ICourseRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index(string name=null,decimal? price=null,string isActive=null)
        {
            // return View(repository.Courses);
            //   return View(repository.Courses.Where(i=>i.isActive==true)); // bu yontem tum verileri ceker ekrana sarta uygun sonucu getiirir cok veri olunca sıkıntıdır.Ve bunun sebebi ise courses classını ienumerabla yapmamız eger bunu iquerable yapsaydık sarta uygun verileri cekerdi repository tarafında.
            // var courses = repository.Courses.Where(i => i.isActive == true).ToList(); //tolist demezsen sorguyu iki kere calıştırır bu da sistem icin iyi degil
            //  var courses = repository.GetCoursesByActive(true);//repositorycourse yaptıktan sonta burayı artık boyle cagırabilriz filtreleme eklediketen sorna burası iptal
            var courses = repository.GetCoursesByFilters(name, price, isActive);
            //ViewBag.CourseCount = courses.Count();  filtreleme eklediketen sorna burası iptal
            ViewBag.Name = name;
            ViewBag.Price = price;
            ViewBag.isActive = isActive=="on"?true:false;            

            return View(courses);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.ActionMode = "Edit";
            return View(repository.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Course entity,Course orginal)
        {
            repository.UpdateCourse(entity,orginal);

            return RedirectToAction(nameof(Index));
        }
        #region Create
        public IActionResult Create()
        {
            ViewBag.ActionMode = "Create";
            return View("Edit",new Course());
        }
        [HttpPost]
        public IActionResult Create(Course newCourse)
        {
           int id= repository.CreateCourse(newCourse);
            Console.WriteLine("Id: {0}", id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        [HttpPost]
        public IActionResult Delete(int id)
        {
           
            repository.DeleteCourse(id);
            return RedirectToAction("Index");
        }
    }
}