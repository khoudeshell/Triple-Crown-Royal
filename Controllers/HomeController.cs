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
            Logger.LogInfo(string.Format("User: {0}, form: {1}", this.User.Identity.Name,
                getFormCollection(collection)));

            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);

            if (!leagueRace.IsUpdateable)
            {
                ModelState.AddModelError("_FORM", "This race is no longer eligible to change");
                return View();
            }

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

            if (!this.UserLeague.IsValidRaceCondition(leagueRace))
            {
                ModelState.AddModelError("_FORM", "Put a separate horse for each bet type");
                return View();
            }

            this.userLeagueRepository.SaveOrUpdate(this.UserLeague);
            this.ViewData["SuccessMessage"] = "Picks updated successfully";
            Logger.LogInfo(string.Format("Saved picks for User: {0}", this.User.Identity.Name));

            Emailer.SendEmail(UserLeague, membershipService.GetUser(this.HorseUser.UserName).Email, leagueRace);
            return View();
        }

        private string getFormCollection(FormCollection collection)
        {
            string vals = string.Empty;

            foreach (var key in collection.AllKeys)
            {
                vals += string.Format("key:{0},value{1};", key.ToString(), collection[key].ToString());
            }

            return vals;
        }
    }
}
