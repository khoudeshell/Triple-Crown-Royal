using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models;
using HorseLeague.Models.Domain;

namespace HorseLeague.Controllers
{
    public class ResultsController : HorseLeagueController
    {
        public ResultsController() { }

        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["AllResults"] = this.UserLeague.League.LeagueRaces.Where(x => x.RaceDetailPayouts.Count > 0);

            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            IList<ReportLeagueRaceBet> results = leagueRace.ReportLeagueRaceBets;

            this.ViewData.Model = new LeagueRaceReport(results);
            this.ViewData["LeagueRaceDomain"] = leagueRace;

            return View();
        }
    }
}
