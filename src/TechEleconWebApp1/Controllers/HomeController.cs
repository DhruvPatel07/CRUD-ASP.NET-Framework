using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechEleconWebApp1.Models;

namespace TechEleconWebApp1.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db= new StudentContext();
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid == true)
            {
            db.Students.Add(s);
            int a = db.SaveChanges();
            if (a > 0)
            {
                    //viewbag.insertmessage = "<script>alert('data inserted!!!')</script>";

                    TempData["InsertMessage"] = "<script>alert('data inserted!!!')</script>";
                    return RedirectToAction("Index");
                }
            else
            {
                ViewBag.InsertMessage = "<script>alert('Data NOT Inserted!!!')</script>";

            }

            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
           db.Entry(s).State= EntityState.Modified;
            int a= db.SaveChanges();
            if (a > 0)
            {
                ViewBag.UpdateMessage= "<script>alert('data Updated!!!')</script>";
                ModelState.Clear();
            }
            else
            {
                ViewBag.UpdateMessage = "<script>alert('data NOT Updated!!!')</script>";
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var StudentIdRow = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(StudentIdRow);
        }

        [HttpPost]
        public ActionResult Delete(Student s)
        {
            db.Entry(s).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }









        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}