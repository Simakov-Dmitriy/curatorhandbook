using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using studentCurator.Models;

namespace studentCurator.Controllers
{
    [Authorize()]
    public class StudentController : Controller
    {
        private readonly ApplicationContext db;

        public StudentController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Students(int? id)
        {
            ViewBag.GroupId = id;
            return View(db);
        }
        public IActionResult AddStudent(int? id)
        {
            var currentGroup = db.Groups.FirstOrDefault(i => i.Id == id);
            ViewBag.GroupId = id;
            ViewBag.Faculty = currentGroup?.Faculty;
            ViewBag.Course = currentGroup?.Course;
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = 0;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Students", new {id = db.Groups.FirstOrDefault(i => i.Id == student.GroupId)?.Id});
            }
            ViewBag.GroupId = student.GroupId;
            ViewBag.Faculty = student.Faculty;
            ViewBag.Course = student.Course;
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var model = db.Students.FirstOrDefault(i => i.Id == id);
                if (model != null)
                {
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Update(student);
                db.SaveChanges();
                return RedirectToAction("Students", new {id = student.GroupId});
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                var item = db.Students.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    db.Students.Remove(item);
                    db.SaveChanges();
                    return RedirectToAction("Students", new {id = item.GroupId});
                }
            }

            return NotFound();
        }

        public IActionResult Marks(int id)
        {
            ViewBag.GroupId = id;
            return View(db);
        }

        public IActionResult GetMarks(int student, int subject, int mark)
        {
            if (mark > 100)
            {
                mark = 100;
            }
            MarkModel element = new MarkModel
            {
                StudentId = student,
                SubjectId = subject,
                Mark = mark
            };
            var currentGroupId = db.Students.FirstOrDefault(i => i.Id == student)?.GroupId;
            var before = db.Marks.FirstOrDefault(i => i.StudentId == student && i.SubjectId == subject);
            if (before != null)
                db.Marks.Remove(before);
            db.Marks.Add(element);
            db.SaveChanges();
            return RedirectToAction("Marks", new {id = currentGroupId});
        }
    }
}