using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models.Domain;

namespace HorseLeague.Controllers
{
    public class StandingsController : HorseLeagueController
    {
        [Authorize]
        public ActionResult Index()
        {
            this.ViewData.Model = this.UserLeague.League;
            
            return View();
        }

        [Authorize]
        public ActionResult Details(System.Guid id)
        {
            User u = this.UserRepository.Get(id);
            UserLeague ul = u.GetUserLeague(this.UserLeague.League);

            this.ViewData["UserLeague"] = ul;
            IList<UserRaceDetail> raceResults = ul.UserRaceDetails;

            this.ViewData["UserRaceResults"] = raceResults.Select(x => x.RaceDetail.LeagueRace).Distinct().ToList();

            return View();
        }
    }
}
