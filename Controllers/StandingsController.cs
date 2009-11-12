using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;

namespace HorseLeague.Controllers
{
    public class StandingsController : HorseLeagueController
    {
        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["UserStandings"] = base.Repository.GetUserStandings(DateTime.Now);

            return View();
        }

        [Authorize]
        public ActionResult Details(System.Guid id)
        {
            aspnet_User user = Repository.GetUser(id);

            this.ViewData["User"] = user;
            this.ViewData["UserRaceResults"] = base.Repository.GetUserResults(id);

            return View();
        }
    }
}
