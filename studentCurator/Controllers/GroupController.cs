using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using studentCurator.Models;

namespace studentCurator.Controllers
{
    [Authorize()]
    public class GroupController : Controller
    {
        private readonly ApplicationContext db;

        public GroupController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Groups()
        {
            return View(db);
        }
        public IActionResult AddGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Groups");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Group model = db.Groups.FirstOrDefault(i => i.Id == id);
                if (model != null)
                {
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Update(group);
                db.SaveChanges();
                return RedirectToAction("Groups");
            }

            return View(group);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Group item = db.Groups.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    foreach (var student in db.Students.Where(i => i.GroupId == id))
                    {
                        db.Students.Remove(student);
                    }
                    db.Remove(item);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Groups");
        }
    }
}