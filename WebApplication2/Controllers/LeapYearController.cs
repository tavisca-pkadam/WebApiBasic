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

        

        // GET: LeapYear/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        
        
    }
}