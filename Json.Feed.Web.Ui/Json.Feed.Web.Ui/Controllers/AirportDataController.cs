using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Json.Feed.Web.Ui.Controllers
{
    public class AirportDataController : BaseController
    {
        // GET: AirportData
        public ActionResult AirportInfo()
        {
            return View();
        }
    }
}