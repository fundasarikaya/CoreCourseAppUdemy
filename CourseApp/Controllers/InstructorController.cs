using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepository _repository;//dependency injection
        private ICourseRepository _courseRepository;
        public InstructorController(IInstructorRepository repository, ICourseRepository courseRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            ViewBag.InstructorEditId = TempData["InstructorEditId"];
            ViewBag.InstructorCreateId = TempData["InstructorCreateId"];
            ViewBag.InstructorChangeId = TempData["InstructorChangeId"];
            return View(_repository.GetAll()); //bu sekide yaptıgımızda tablodaki herseyi getirir sonra ikinci sorguda istenieni geitiryor ikkinci sorguda herhangi bir filtre yok biz fltre ugulamak istersek efinstructorrepository filtre kısmına bak
        }
        public IActionResult Edit(int id)
        {
            TempData["InstructorEditId"] = id;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Instructor entity)
        {
            _repository.Update(entity);
            return RedirectToAction("Index");
        }
        public IActionResult Create(int id)
        {
            TempData["InstructorCreateId"] = id;
            return RedirectToAction("Index");
        }
        
        public IActionResult Change(int id)
        {
            TempData["InstructorChangeId"] = id;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Change(int id,Course[] courses)
        {
            _courseRepository.UpdateAll(id, courses);
            return RedirectToAction("Index");
        }
    }
}
/*
 * Her ekledigimiz uygulamaya her ekledigimiz enttiy icin ayrı ayrı repository ekklemek yerine generic repository   oluşturup tek bir repositroy ile entity sunıfları ile calışabilriz
 */