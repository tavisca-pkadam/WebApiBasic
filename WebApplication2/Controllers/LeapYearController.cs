using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2
{
    [Route("/api/leapyear")]
    public class LeapYearController : Controller
    {

        // GET: LeapYear/Details/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            string result = "";
            if(id %4 == 0)
            {
                result = "It is a Leap Year";
            }
            else
            {
                result = "It is not a Leap Year";
            }
            return Json(new { Year = result });
        }

        // GET: LeapYear/Create
        public ActionResult Create()
        {
            return Json(new { nextYear = "2020" });
        }

        // POST: LeapYear/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeapYear/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeapYear/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeapYear/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeapYear/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}