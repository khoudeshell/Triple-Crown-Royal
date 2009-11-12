using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorseLeague.Models;
using HorseLeague.Models.DataAccess;

namespace HorseLeague.Controllers
{
    [HandleError]
    public class HomeController : HorseLeagueController
    {
        public HomeController() : base() { }

        public HomeController(IHorseLeagueRepository repository) : base(repository) { }

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
            //populatePicks(id, Convertor);

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
            return View();
        }
    }
}
