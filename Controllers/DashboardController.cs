using BoiMela.DataAccess;
using BoiMela.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            SummaryStatDtos summaryStat = SummaryStatDataAccess.GetSummaryStatDataAccess();
            return View(summaryStat);
        }
    }
}