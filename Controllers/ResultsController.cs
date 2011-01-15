using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models;

namespace HorseLeague.Controllers
{
    public class ResultsController : HorseLeagueController
    {
        public ResultsController() : this(null) { }

        public ResultsController(IHorseLeagueRepository repository) : base(repository) { }

        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["AllResults"] = this.Repository.GetResults();

            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            IList<ReportLeagueRaceBet> results = this.Repository.GetLeagueRaceBetReport(id);

            //IList<ReportLeagueRaceBet> win = this.Repository.GetLeagueRaceBetReport(id, HorseLeague.Models.BetTypes.Win);
            //IList<ReportLeagueRaceBet> place = this.Repository.GetLeagueRaceBetReport(id, HorseLeague.Models.BetTypes.Place);
            //IList<ReportLeagueRaceBet> show = this.Repository.GetLeagueRaceBetReport(id, HorseLeague.Models.BetTypes.Show);

            this.ViewData.Model = new LeagueRaceReport(results);
            this.ViewData["LeagueRaceDomain"] = new LeagueRaceDomain(this.Repository.GetLeagueRace(id), this.Repository);

            return View();
        }
    }
}
