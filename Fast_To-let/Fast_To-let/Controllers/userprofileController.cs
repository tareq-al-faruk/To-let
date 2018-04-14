using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fast_To_let.Controllers
{
    public class userprofileController : Controller
    {
        // GET: userprofile
        [Authorize]
        public ActionResult userpage()
        {
            return View();
        }
    }
}