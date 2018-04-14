using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast_To_let.Controllers
{
    public class adminpanelController : Controller
    {
        // GET: adminpanel
        [Authorize]
        public ActionResult admin()
        {
            return View();
        }
    }
}