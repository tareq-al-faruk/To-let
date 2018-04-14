using Fast_To_let.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast_To_let.Controllers
{
    public class AddinfoController : Controller
    {
        // GET: Addinfo
        [HttpGet]
        public ActionResult save_addinfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult save_addinfo(AddInfo add)
        {
            string filename = Path.GetFileNameWithoutExtension(add.Image.FileName);
            string extension = Path.GetExtension(add.Image.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            add.photo = "~/imagefile/" + filename;
            filename = Path.Combine(Server.MapPath("~/imagefile/"), filename);
            add.Image.SaveAs(filename);
            using (fast_tolet db = new fast_tolet())
            {
                db.AddInfoes.Add(add);
                db.SaveChanges();
                RedirectToAction("Index","Message");
            }

                return View();
        } 
       
       
    }
   
}