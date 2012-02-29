using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models;
using HorseLeague.Models.Domain;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;
using HorseLeague.Helpers;

namespace HorseLeague.Controllers
{
    [Authorize(Users = "kurt,stephanie")]
    public class AdminController : HorseLeagueController
    {
        private readonly IHorseRepository horseRepository;
        private readonly IRepository<LeagueRace> leagueRaceRepository;
        private readonly IRepository<RaceDetail> raceDetailRepository;
        private readonly IRepository<UserLeague> userLeagueRepository;
        private readonly ILeagueRepository leagueRepository;

        public AdminController(IHorseRepository horseRepository, 
            IRepository<LeagueRace> leagueRaceRepository,
            IRepository<RaceDetail> raceDetailRepository,
            ILeagueRepository leagueRepository,
            IRepository<UserLeague> userLeagueRepository)
        {
            this.horseRepository = horseRepository;
            this.leagueRaceRepository = leagueRaceRepository;
            this.raceDetailRepository = raceDetailRepository;
            this.leagueRepository = leagueRepository;
            this.userLeagueRepository = userLeagueRepository;
        }

        public ActionResult Index()
        {
            this.ViewData["ScheduledRaces"] = this.UserLeague.League.LeagueRaces;
            this.ViewData["Users"] = this.UserRepository.GetAll();
            
            return View();
        }

        public ActionResult ViewLeagueRace(int id)
        {
            initializeViewLeagueRace(id);

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction]
        public ActionResult ViewLeagueRace(int id, FormCollection collection)
        {
            LeagueRace domain = initializeViewLeagueRace(id);
            
            domain.PostTimeEST = Convert.ToDateTime(collection["txtPost"]);
            domain.IsActive = Convert.ToInt32(collection["txtIsActive"]);
            domain.FormUrl = Convert.ToString(collection["txtForm"]);

            this.leagueRaceRepository.SaveOrUpdate(domain);

            return View();
        }

        private LeagueRace initializeViewLeagueRace(int id)
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            ViewData.Model = leagueRace;

            IList<User> usersWithScratches = this.UserRepository.GetUsersWithScratches(leagueRace);

            this.ViewData["UserScratches"] = usersWithScratches;
            this.ViewData[BetTypes.Exacta.ToString()] = leagueRace.RaceExoticPayouts.Where(x => x.BetType == BetTypes.Exacta).ToList();
            this.ViewData[BetTypes.Trifecta.ToString()] = leagueRace.RaceExoticPayouts.Where(x => x.BetType == BetTypes.Trifecta).ToList();

            return leagueRace;
        }

       
        public ActionResult AddHorse(int id)
        {
            RaceDetail rd = new RaceDetail();
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            rd.LeagueRace = leagueRace;

            this.ViewData.Model = rd;

            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction(RollbackOnModelStateError=true)]
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

            try
            {

                Horse horse = this.horseRepository.GetHorseByName(horseName);
                if (horse == null)
                {
                    //Horse didn't exist add it
                    horse = new Horse();
                    horse.Name = horseName;

                    horse = this.horseRepository.SaveOrUpdate(horse);
                }

                LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);

                //Add the new race detail
                RaceDetail detail = new RaceDetail();
                detail.Horse = horse;
                detail.LeagueRace = leagueRace;
                detail.PostPosition = post;
                raceDetailRepository.SaveOrUpdate(detail);

                //leagueRace.RaceDetails.Add(detail);
               // leagueRaceRepository.SaveOrUpdate(leagueRace);

                return RedirectToAction("ViewLeagueRace", new { id = id });
            }
            catch
            {
                this.leagueRaceRepository.DbContext.RollbackTransaction();
                throw;
            }
            
        }


        public ActionResult AddExoticPayout(int id, BetTypes bet)
        {
            this.ViewData.Model = leagueRaceRepository.Get(id);
            this.ViewData["BetType"] = bet;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction(RollbackOnModelStateError = true)]
        public ActionResult AddExoticPayout(int id, BetTypes bet, FormCollection collection)
        {
            LeagueRace leagueRace = this.leagueRaceRepository.Get(id);
            string amountTemp = collection["Amount"];
            float amount = float.MinValue;
            if(float.TryParse(amountTemp, out amount))
            {
                RaceExoticPayout payout = new RaceExoticPayout()
                {
                    Amount = amount,
                    BetType = bet,
                    LeagueRace = leagueRace
                };

                leagueRace.RaceExoticPayouts.Add(payout);
                leagueRaceRepository.SaveOrUpdate(leagueRace);
            }

            return RedirectToAction("ViewLeagueRace", new { id = id }); 
        }

        public ActionResult EditHorse(int id)
        {
            this.ViewData.Model = raceDetailRepository.Get(id);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction]
        public ActionResult EditHorse(int id, FormCollection collection)
        {
            RaceDetail rd = raceDetailRepository.Get(id);
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

            this.raceDetailRepository.SaveOrUpdate(rd);

            return RedirectToAction("ViewLeagueRace", new { id = rd.LeagueRace.Id }); 
        }

        [Authorize]
        public ActionResult AddPayout(int id, BetTypes bet)
        {
            this.initializePayout(id, bet);
        
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction]
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
            float winAmount = float.MinValue;
            if (isWinEnabled(payout) && !float.TryParse(winAmountTemp, out winAmount))
            {
                ModelState.AddModelError("WinAmount", "The win amount can not be blank");
                return View();
            }

            string placeAmountTemp = collection["PlaceAmount"];
            float placeAmount = float.MinValue;
            if (isPlaceEnabled(payout) && !float.TryParse(placeAmountTemp, out placeAmount))
            {
                ModelState.AddModelError("PlaceAmount", "The place amount can not be blank");
                return View();
            }

            string showAmountTemp = collection["ShowAmount"];
            float showAmount = float.MinValue;
            if (!float.TryParse(showAmountTemp, out showAmount))
            {
                ModelState.AddModelError("ShowAmount", "The show amount can not be blank");
                return View();
            }
            #endregion

            RaceDetail raceDetail = raceDetailRepository.Get(raceDetailId);
            payout.RaceDetail = raceDetail;
            payout.WinAmount = payout.BetType == BetTypes.Win ? (float?)winAmount : null;
            payout.PlaceAmount = payout.BetType != BetTypes.Show ? (float?)placeAmount : null;
            payout.ShowAmount = showAmount;

            raceDetail.RaceDetailPayout.Add(payout);

            this.raceDetailRepository.SaveOrUpdate(raceDetail);

            return RedirectToAction("ViewLeagueRace", new { id = id }); 

        }

        private RaceDetailPayout initializePayout(int id, BetTypes bet)
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            RaceDetailPayout payout = new RaceDetailPayout()
            {
                BetType = bet,
                LeagueRace = leagueRace
            };
            
            this.ViewData.Model = payout;
            this.ViewData["LeagueRace"] = leagueRace;
            this.ViewData["IsWinEnabled"] = isWinEnabled(payout);
            this.ViewData["IsPlaceEnabled"] = isPlaceEnabled(payout);

            return payout;
        }

        //private RaceDetailPayout createPayout(LeagueRace leagueRace, BetTypes bet)
        //{
        //    RaceDetailPayout payout = new RaceDetailPayout();

        //    payout.BetType = bet;
            
        //    return payout;
        //}

        private bool isWinEnabled(RaceDetailPayout payout)
        {
            return BetTypes.Win == payout.BetType;
        }

        private bool isPlaceEnabled(RaceDetailPayout payout)
        {
            return BetTypes.Show != payout.BetType;
        }

        public ActionResult FixUserPicks(int id, System.Guid userId)
        {
            User user = this.UserRepository.Get(userId);
            this.ViewData["UserDomain"] = user.GetUserLeague(this.UserLeague.League);
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);

            leagueRace.IsUpdateable = true;
            this.ViewData.Model = leagueRace;
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Transaction]
        public ActionResult FixUserPicks(int id, System.Guid userId, FormCollection collection)
        {
            LeagueRace leagueRace = this.UserLeague.League.GetLeagueRace(id);
            User user = this.UserRepository.Get(userId);
            UserLeague userLeague = user.GetUserLeague(leagueRace.League);
            this.ViewData["UserDomain"] = userLeague;
            this.ViewData.Model = leagueRace;
            
            userLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbWin"])).First(),
                BetTypes.Win);
            userLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbPlace"])).First(),
                BetTypes.Place);
            userLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbShow"])).First(),
                BetTypes.Show);
            userLeague.AddUserPick(leagueRace,
                leagueRace.RaceDetails.Where(x => x.Id == Convert.ToInt32(collection["cmbBackUp"])).First(),
                BetTypes.Backup);

            if (!userLeague.IsValidRaceCondition(leagueRace))
            {
                ModelState.AddModelError("_FORM", "Put a separate horse for each bet type");
                return View();
            }

            this.userLeagueRepository.SaveOrUpdate(userLeague);
            
            return View();
        }

        public ActionResult TestEmail(string email)
        {
            Emailer.SendEmail(new EmailTester(), email, null);

            return null;
        }

        private class EmailTester : IEmailable
        {
            public string GetSubject(LeagueRace leagueRace)
            {
                return "Test Subject";
            }

            public string GetBody(LeagueRace leagueRace)
            {
                return "Test Body";
            }
        }

        [Transaction]
        public ActionResult RecalcStandings()
        {
            leagueRepository.RecalculateStandings(this.UserLeague.League);

            IList<User> users = this.UserRepository.GetAll();

            foreach (User user in users)
            {
                UserLeague userLeague = user.GetUserLeague(this.UserLeague.League);
                if (userLeague != null)
                {
                    foreach (LeagueRace leagueRace in userLeague.League.LeagueRaces)
                    {
                        RaceExoticPayout raceExactaPayout = leagueRace.RaceExoticPayouts.Where(x => x.BetType == BetTypes.Exacta).FirstOrDefault();
                        RaceExoticPayout raceTrifectaPayout = leagueRace.RaceExoticPayouts.Where(x => x.BetType == BetTypes.Trifecta).FirstOrDefault();

                        if (this.AddExoticWinner(userLeague, leagueRace, raceExactaPayout,
                            (LeagueRace lr) =>
                            {
                                return userLeague.WasExactaWinner(lr);
                            })
                            &&
                            this.AddExoticWinner(userLeague, leagueRace, raceTrifectaPayout,
                                (LeagueRace lr) =>
                                {
                                    return userLeague.WasTrifectaWinner(lr);
                                }))
                        {
                            this.leagueRepository.SaveOrUpdate(userLeague.League);
                        }
                    }
                }
            }

            return View();
        }

        private bool AddExoticWinner(UserLeague userLeague,
            LeagueRace leagueRace, RaceExoticPayout payout,
            Func<LeagueRace, bool> wasWinner)
        {
            if (wasWinner(leagueRace))
            {
                userLeague.League.UserRaceExoticPayouts.Add(new UserRaceExoticPayout()
                {
                    RaceExoticPayout = payout,
                    UserLeague = userLeague,
                    League = userLeague.League
                });

                return true;
            }

            return false;
        }

       
    }
}

