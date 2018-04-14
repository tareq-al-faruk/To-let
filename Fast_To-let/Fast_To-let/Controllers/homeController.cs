using Fast_To_let.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fast_To_let.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            using (fast_tolet db = new fast_tolet())
            {


                return View(db.AddInfoes.Where(a => a.IsApproved == true).ToList());
            }
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            using (fast_tolet db = new fast_tolet())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                } 
                AddInfo addInfo = db.AddInfoes.Find(id);
               if (addInfo == null)
                {
                    return HttpNotFound();
                } 
                return View(addInfo);
            }
        }
        [HttpGet]
        public ActionResult show(int id)
        {
            AddInfo td = new AddInfo();
            using (fast_tolet db = new fast_tolet())
            {

                //  return View(db.Tables.ToList());
                td = db.AddInfoes.Where(x => x.add_id == id).FirstOrDefault();
            }
            return View(td);
        }


    }
}