using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorseLeague.Models;
using HorseLeague.Models.DataAccess;
using HorseLeague.Helpers;
using HorseLeague.Models.Domain;
using SharpArch.Web.NHibernate;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Core.PersistenceSupport;


namespace HorseLeague.Controllers
{
    [HandleError]
    public class HomeController : HorseLeagueController
    {
        private readonly IMembershipService membershipService;
        private readonly IRepository<UserLeague> userLeagueRepository;
        
        public HomeController(IMembershipService membershipService,
            IRepository<UserLeague> userLeagueRepository) : 
            base() 
        {
            this.membershipService = membershipService ?? new AccountMembershipService();
            this.userLeagueRepository = userLeagueRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["ActiveRaces"] = this.UserLeague.League.ActiveRaces;
            this.ViewData["UserDomain"] = this.UserLeague;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        public ActionResult Picks(int id) 
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            this.ViewData.Model = leagueRace;
            this.ViewData["UserDomain"] = this.UserLeague;

            return View(); 
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction]
        public ActionResult Picks(int id, FormCollection collection)
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);

            this.UserLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbWin"])).First(),
                BetTypes.Win);
            this.UserLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbPlace"])).First(),
                BetTypes.Place);
            this.UserLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbShow"])).First(),
                BetTypes.Show);
            this.UserLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbBackUp"])).First(),
                BetTypes.Backup);

            this.ViewData.Model = leagueRace;
            this.ViewData["UserDomain"] = this.UserLeague;

            if (!leagueRace.IsUpdateable)
            {
                ModelState.AddModelError("_FORM", "This race is no longer eligible to change");
                return View();
            }

            if (!this.UserLeague.IsValidRaceCondition(leagueRace))
            {
                ModelState.AddModelError("_FORM", "Put a separate horse for each bet type");
                return View();
            }

            this.userLeagueRepository.SaveOrUpdate(this.UserLeague);
            this.ViewData["SuccessMessage"] = "Picks updated successfully";

            Emailer.SendEmail(UserLeague, membershipService.GetUser(this.HorseUser.UserName).Email, leagueRace);
            return View();
        }
    }
}
