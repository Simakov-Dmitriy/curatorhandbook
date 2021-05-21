using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using studentCurator.Models;

namespace studentCurator.Controllers
{
    [Authorize()]
    public class SubjectController : Controller
    {
        private readonly ApplicationContext db;

        public SubjectController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult AttendancePage(int id, string problem)
        {
            ViewBag.GroupId = id;
            ViewBag.Problem = problem;
            return View(db);
        }
        [HttpPost]
        public IActionResult AddSubject(string text, int id)
        {
            var subject = new Subject
            {
                Name = text,
                GroupId = id
            };
            db.Subjects.Add(subject);
            db.SaveChanges();
            return RedirectToAction("AttendancePage", new {id = id});
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                var item = db.Subjects.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    db.Subjects.Remove(item);
                    db.SaveChanges();
                    return RedirectToAction("AttendancePage", new {id = item.GroupId});
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult GetStatistics(string input1, int group, DateTime day)
        {
            if (day != DateTime.MinValue && group != 0)
            {
                var newdaystat = new DayStatistics
                {
                    GroupId = group,
                    Statistics = input1 ?? "0+0",
                    Day = day
                };
                var item = db.DayStatisticsEnumerable.FirstOrDefault(i => i.Day == day);
                if (item != null)
                {
                    db.DayStatisticsEnumerable.Remove(item);
                }
                db.DayStatisticsEnumerable.Add(newdaystat);
                foreach (var student in db.Students.Where(i => i.GroupId == group))
                {
                    student.DaysGone++;
                }
                db.SaveChanges();
                return RedirectToAction("AttendancePage", new {id = group});
            }
            return RedirectToAction("AttendancePage", new {id = group, problem = "Укажите дату!"});
        }

        public IActionResult Reports(int id, DateTime startDate, DateTime endDate, string resultSubject)
        { 
            if (resultSubject != null && id != 0)
            {
                Dictionary<int, string> studentsAttendance = new Dictionary<int, string>();
                foreach (var student in db.Students.Where(i => i.GroupId == id).ToList())
                {
                    studentsAttendance.Add(student.Id, "");
                }
                var subjId = db.Subjects.FirstOrDefault(i => i.GroupId == id && i.Name == resultSubject)?.Id;
                
                    var currentStat = db.DayStatisticsEnumerable.Where(i => i.GroupId == id && i.Day >= startDate && i.Day <= endDate).ToList();
                    if (subjId != 0 && currentStat.Count != 0)
                    {
                        foreach (var stat in currentStat)
                        {
                            foreach (var item in stat.Statistics.Split(","))
                            {
                                var current = item.Split("+");
                                if (Int32.Parse(current[1]) == subjId)
                                {
                                    studentsAttendance[Int32.Parse(current[0])] += " 1 ";
                                }
                            }
                        }
                        foreach (var student in db.Students.Where(i => i.GroupId == id).ToList())
                        {
                            studentsAttendance[student.Id] =
                                studentsAttendance[student.Id].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length
                                    .ToString() + "/" + currentStat.Count.ToString();
                        }
                    }

                    ViewBag.studentsAttendance = studentsAttendance;
            }
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.resultSubject = resultSubject;
            ViewBag.GroupId = id;
            return View(db);
        }
    }
}