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
    [Authorize(Users = "kurt,stephanie")]
    public class AdminController : HorseLeagueController
    {
        [OutputCache(Duration=86400, VaryByParam="none")]
        public ActionResult Index()
        {
            this.ViewData["ScheduledRaces"] = this.Repository.GetAllRaces();
            this.ViewData["Users"] = this.Repository.GetAllUsers();
            
            return View();
        }

        public ActionResult ViewLeagueRace(int id)
        {
            initializeViewLeagueRace(id);

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewLeagueRace(int id, FormCollection collection)
        {
            LeagueRaceDomain domain = initializeViewLeagueRace(id);
            
            domain.PostTimeEST = Convert.ToDateTime(collection["txtPost"]);
            domain.IsActive = Convert.ToInt32(collection["txtIsActive"]);
            domain.FormUrl = Convert.ToString(collection["txtForm"]);

            this.Repository.PersistLeagueRace(domain);

            return View();
        }

        private LeagueRaceDomain initializeViewLeagueRace(int id)
        {
            LeagueRaceDomain lrd = new LeagueRaceDomain(this.Repository.GetLeagueRace(id), this.Repository);
            ViewData.Model = lrd; 
            this.ViewData["UserScratches"] = this.Repository.GetUsersWithScratches(id);
            return lrd;
        }

        public ActionResult AddHorse(int id)
        {
            RaceDetail rd = new RaceDetail();
            rd.LeagueRaceId = id;

            this.ViewData.Model = rd;

            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddHorse(int id, FormCollection collection)
        {
            #region Validation
            string horseName = collection["Horse"];
            if (horseName == null)
            {
                ModelState.AddModelError("Horse", "The horse name can not be blank");
                return View();
            }

            string postPosition = collection["PostPosition"];
            int post;
            if (!Int32.TryParse(postPosition, out post))
            {
                ModelState.AddModelError("PostPosition", "The post position can not be blank");
                return View();
            }
            #endregion

            Horse horse = this.Repository.GetHorse(horseName);
            if (horse == null)
            {
                //Horse didn't exist add it
                horse = new Horse();
                horse.Name = horseName;

                horse = this.Repository.PersistHorse(horse);
            }
            
            //Add the new race detail
            RaceDetail detail = new RaceDetail();
            detail.Horse = horse;
            detail.LeagueRaceId = id;
            detail.PostPosition = post;

            this.Repository.PersistRaceDetail(detail);

            return RedirectToAction("ViewLeagueRace", new { id = id });
        }

        public ActionResult EditHorse(int id)
        {
            this.ViewData.Model = this.Repository.GetRaceDetail(id);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditHorse(int id, FormCollection collection)
        {
            RaceDetail rd = this.Repository.GetRaceDetail(id);
            this.ViewData.Model = rd;
                
            #region Validation
            string oddsOrderTemp = collection["OddsOrder"];
            int oddsOrder;
            if (!Int32.TryParse(oddsOrderTemp, out oddsOrder))
            {
                ModelState.AddModelError("OddsOrder", "The odds order needs to be a number");
                return View();
            }

            string postPosition = collection["PostPosition"];
            int post;
            if (!Int32.TryParse(postPosition, out post))
            {
                ModelState.AddModelError("PostPosition", "The post position can not be blank");
                return View();
            }

            string isScratchedTemp = collection["IsScratched"];
            int isScratched;
            if (!Int32.TryParse(isScratchedTemp, out isScratched) || (isScratched < 0 || isScratched > 1))
            {
                ModelState.AddModelError("IsScratched", "The scratch needs to be a 0 or 1");
                return View();
            }
            #endregion

            rd.OddsOrder = oddsOrder;
            rd.IsScratched = isScratched;
            rd.PostPosition = post;

            this.Repository.UpdateRaceDetail(rd);

            return RedirectToAction("ViewLeagueRace", new { id = rd.LeagueRace.Id }); 
        }

        [Authorize]
        public ActionResult AddPayout(int id, BetTypes bet)
        {
            this.initializePayout(id, bet);
        
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPayout(int id, BetTypes bet, FormCollection collection)
        {
            RaceDetailPayout payout = initializePayout(id, bet);
            
            #region Validation
            string raceDetailIdTemp = collection["RaceDetailId"];
            int raceDetailId;
            if (!Int32.TryParse(raceDetailIdTemp, out raceDetailId))
            {
                ModelState.AddModelError("RaceDetailId", "The horse must be selected");
                return View();
            }
            
            string winAmountTemp = collection["WinAmount"];
            double winAmount = double.MinValue;
            if (isWinEnabled(payout) && !double.TryParse(winAmountTemp, out winAmount))
            {
                ModelState.AddModelError("WinAmount", "The win amount can not be blank");
                return View();
            }

            string placeAmountTemp = collection["PlaceAmount"];
            double placeAmount = double.MinValue;
            if (isPlaceEnabled(payout) && !double.TryParse(placeAmountTemp, out placeAmount))
            {
                ModelState.AddModelError("PlaceAmount", "The place amount can not be blank");
                return View();
            }

            string showAmountTemp = collection["ShowAmount"];
            double showAmount = double.MinValue;
            if (!double.TryParse(showAmountTemp, out showAmount))
            {
                ModelState.AddModelError("ShowAmount", "The show amount can not be blank");
                return View();
            }
            #endregion

            payout.RaceDetailId = raceDetailId;
            payout.WinAmount = (payout.BetType == Convert.ToInt32(BetTypes.Win)) ? (double?)winAmount : null;
            payout.PlaceAmount = (payout.BetType != Convert.ToInt32(BetTypes.Show)) ? (double?)placeAmount : null;
            payout.ShowAmount = showAmount;

            this.Repository.PersistRaceDetailPayout(payout);

            return RedirectToAction("ViewLeagueRace", new { id = id }); 

        }

        private RaceDetailPayout initializePayout(int id, BetTypes bet)
        {
            RaceDetailPayout payout = createPayout(id, bet);
            
            this.ViewData.Model = payout;
            this.ViewData["LeagueRace"] = this.Repository.GetLeagueRace(id);
            this.ViewData["IsWinEnabled"] = isWinEnabled(payout);
            this.ViewData["IsPlaceEnabled"] = isPlaceEnabled(payout);

            return payout;
        }

        private RaceDetailPayout createPayout(int leagueRaceId, BetTypes bet)
        {
            RaceDetailPayout payout = new RaceDetailPayout();

            payout.BetType = Convert.ToInt32(bet);
            payout.LeagueRaceId = leagueRaceId;

            return payout;
        }

        private bool isWinEnabled(RaceDetailPayout payout)
        {
            return Convert.ToInt32(BetTypes.Win) == payout.BetType;
        }

        private bool isPlaceEnabled(RaceDetailPayout payout)
        {
            return Convert.ToInt32(BetTypes.Show) != payout.BetType;
        }

        public ActionResult FixUserPicks(int id, System.Guid userId)
        {
            this.ViewData.Model = new AdminUserLeagueRacePicksDomain(userId, id, this.Repository);
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FixUserPicks(int id, System.Guid userId, FormCollection collection)
        {
            UserLeagueRacePicksDomain userDomain = new AdminUserLeagueRacePicksDomain(userId, id, this.Repository);

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

        public ActionResult RecalcStandings()
        {
            this.Repository.RecalculateStandings();

            return View();
        }
    }
}
