using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Controllers
{
    public class StudentCourseController : Controller
    {
        private DataContext _context;
        public StudentCourseController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new StudentCourseVModel();
            model.Courses = _context
                .Courses
                .Include(i => i.StudentCourses)
                .ThenInclude(i => i.Student)
                //.Where(i=>i.isActive)
                .ToList();
            model.Students = _context
                .Students
                .Include(i => i.StudentCourses)
                .ThenInclude(i => i.Course)
                //.Where(i=>i.StudentCourses
                //    .Any(a=>a.Course.isActive)) //onaylı bir kursa ait olan ogrencileri getiriz
                .ToList();
            return View(model);
        }
        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.Find(id);
            ViewBag.Courses = _context.Courses.Include(p => p.StudentCourses);
            return View("StudentEditor", student);
        }
        [HttpPost]
        public IActionResult EditStudent(int id,int[] courseid)
        {
            Student student = _context
                .Students
                .Include(s => s.StudentCourses)
                .First(i => i.Id == id);
            if(student!=null)
            {
                student.StudentCourses = courseid.Select(cid => new StudentCourse()
                {
                    CourseId=cid,
                    StudentId=id
                }).ToList();
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}