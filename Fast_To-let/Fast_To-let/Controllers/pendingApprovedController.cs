using Fast_To_let.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast_To_let.Controllers
{
    public class pendingApprovedController : Controller
    {
        // GET: pendingApproved
        public ActionResult Index()
        {
            using (fast_tolet dc = new fast_tolet())
            {
                return View(dc.AddInfoes.ToList());
            }
                
        }

        //
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (fast_tolet db = new fast_tolet())
            {


                return View(db.AddInfoes.Where(p => p.add_id == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Edit(AddInfo post)
        {
            using (fast_tolet db = new fast_tolet())
            {

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}