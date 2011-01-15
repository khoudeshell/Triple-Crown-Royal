using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;

namespace HorseLeague.Controllers
{
    public class ScheduleController : HorseLeagueController
    {
        [OutputCache(Duration = 2592000, VaryByParam = "none")]
        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["ScheduledRaces"] = this.Repository.GetAllRaces();

            return View();
        }

    }
}
