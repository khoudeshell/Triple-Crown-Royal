using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorseLeague.Models;
using HorseLeague.Models.DataAccess;
using HorseLeague.Helpers;

namespace HorseLeague.Controllers
{
    [HandleError]
    public class HomeController : HorseLeagueController
    {
        private IMembershipService membershipService;

        public HomeController() : this(null, null) { }

        public HomeController(IHorseLeagueRepository repository, IMembershipService membershipService) : 
            base(repository) 
        {
            this.membershipService = membershipService ?? new AccountMembershipService();
        }

        [Authorize]
        public ActionResult Index()
        {
            this.ViewData["ActiveRaces"] = this.Repository.GetActiveRaces();
            this.ViewData["UserDomain"] = new UserDomain(this.Repository.GetUser(base.Convertor.UserId));
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        public ActionResult Picks(int id) 
        {
            this.ViewData.Model = new UserLeagueRacePicksDomain(Convertor.UserId, id, this.Repository);
          
            return View(); 
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Picks(int id, FormCollection collection)
        {
            UserLeagueRacePicksDomain userDomain = new UserLeagueRacePicksDomain(this.Convertor.UserId, id, this.Repository);

            userDomain.UserPicks.Clear();
            userDomain.AddUserPick(Convert.ToInt32(collection["cmbWin"]), BetTypes.Win);
            userDomain.AddUserPick(Convert.ToInt32(collection["cmbPlace"]), BetTypes.Place);
            userDomain.AddUserPick(Convert.ToInt32(collection["cmbShow"]), BetTypes.Show);
            userDomain.AddUserPick(Convert.ToInt32(collection["cmbBackUp"]), BetTypes.Backup);

            this.ViewData.Model = userDomain;
            if (!userDomain.IsValidRaceCondition)
            {
                ModelState.AddModelError("_FORM", "Put a separate horse for each bet type");
                return View();
            }

            userDomain.UpdatePicks();
            this.ViewData["SuccessMessage"] = "Picks updated successfully";

            Emailer.SendEmail(userDomain, membershipService.GetUser(this.User.Identity.Name).Email);
            return View();
        }
    }
}
