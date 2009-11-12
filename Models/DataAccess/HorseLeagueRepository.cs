using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using HorseLeague.Cache;

namespace HorseLeague.Models.DataAccess
{
    public class HorseLeagueRepository : HorseLeague.Models.DataAccess.IHorseLeagueRepository
    {
        private readonly HorseLeagueDAODataContext dataContext;
        private readonly ICacheManager cache;

        public event EventHandler<UserPicksEventArgs> OnPicksSet;
        public event EventHandler<LeagueRaceEventArgs> OnLeagueRaceChange;
        public event EventHandler<RaceDetailEventArgs> OnRaceDetailChange;
        public event EventHandler<UserStandingsEventArgs> OnStandingsRecalculated;

        public HorseLeagueRepository() 
        {
            this.dataContext = new HorseLeagueDAODataContext();
            this.cache = CacheFactory.GetCacheManager();

            this.OnPicksSet += new EventHandler<UserPicksEventArgs>(HorseLeagueRepository_OnPicksSet);
            this.OnLeagueRaceChange += new EventHandler<LeagueRaceEventArgs>(HorseLeagueRepository_OnLeagueRaceChange);
            this.OnRaceDetailChange += new EventHandler<RaceDetailEventArgs>(HorseLeagueRepository_OnRaceDetailChange);
            this.OnStandingsRecalculated += new EventHandler<UserStandingsEventArgs>(HorseLeagueRepository_OnStandingsRecalculated);
        }

        #region IHorseLeagueRepository Members

        public IList<LeagueRace> GetActiveRaces()
        {
            return cache.Get(ActiveRacesCacheKey.Create(), () =>
            {
                var leagueRaces = from l in dataContext.LeagueRaces
                                  where l.IsActive == 1
                                  select l;

                return leagueRaces.ToList();
            });
        }

        public IList<LeagueRace> GetAllRaces()
        {
            return cache.Get(AllRacesCacheKey.Create(), () =>
            {
                var leagueRaces = from l in dataContext.LeagueRaces
                                  orderby l.Dt
                                  select l;

                return leagueRaces.ToList();
            });
        }

        public LeagueRace GetLeagueRace(int id)
        {
            return cache.Get(LeagueRaceCacheKey.Create(id), () =>
            {
                LeagueRace leagueRace = (from lr in dataContext.LeagueRaces
                                         where lr.Id == id
                                         select lr).First();

                return leagueRace;
            });
        }

        public IList<LeagueRace> GetResults()
        {
            return cache.Get(ResultsCacheKey.Create(), () =>
            {
                var results = from lr in dataContext.LeagueRaces
                              join rdp in dataContext.RaceDetailPayouts on lr.Id equals rdp.LeagueRaceId
                              select lr;

                return results.ToList();
            });
        }

        public IList<UserStanding> GetUserStandings(DateTime leagueYear)
        {
            return cache.Get(UserStandingsCacheKey.Create(), () =>
            {
                return (from us in dataContext.UserStandings
                        where us.Yr.Year == leagueYear.Year
                        orderby us.Total descending
                        select us).ToList();
            });
        }

        public IList<LeagueRace> GetUserResults(Guid userId)
        {
            return cache.Get(UserResultsCacheKey.Create(userId), () =>
            {
                var userPicks = (from lr in dataContext.LeagueRaces
                                 join rd in dataContext.RaceDetails on lr.Id equals rd.LeagueRaceId
                                 join urd in dataContext.UserRaceDetails on rd.RaceDetailId equals urd.RaceDetailId
                                 where urd.UserId == userId
                                 select lr).Distinct();

                return userPicks.ToList();
            });
        }

        public IList<aspnet_User> GetAllUsers()
        {
            return cache.Get(AllUsersCacheKey.Create(), () =>
            {
                var users = from u in dataContext.aspnet_Users
                            select u;

                return users.ToList();
            });
        }

        public IList<aspnet_User> GetUsersWithScratches(int leagueRaceId)
        {
            var userScratches = from u in dataContext.aspnet_Users 
                            join urd in dataContext.UserRaceDetails on u.UserId equals urd.UserId
                            join rd in dataContext.RaceDetails on urd.RaceDetailId equals rd.RaceDetailId
                            where (urd.LeagueRaceId == leagueRaceId) && (rd.IsScratched == 1)
                            select u;

            return userScratches.ToList();
        }

        public IList<UserRaceDetail> GetUserPicks(int leagueRaceId, Guid userId)
        {
            var userPicks = from urd in dataContext.UserRaceDetails
                            where urd.RaceDetail.LeagueRaceId == leagueRaceId && urd.UserId == userId
                            select urd;

            return userPicks.ToList();
        }

        public IList<RaceDetail> GetScratches(int leagueRaceId)
        {
            return cache.Get(LeagueRaceScratchesCacheKey.Create(leagueRaceId), () =>
            {
                return (from rd in dataContext.RaceDetails
                                  where rd.IsScratched == 1 && rd.LeagueRaceId == leagueRaceId
                            select rd).ToList();

            });
        }

        public IList<ReportLeagueRaceBet> GetLeagueRaceBetReport(int leagueRacedId, BetTypes payoutType)
        {
            return cache.Get(LeagueRaceBetTypeReportCacheKey.Create(leagueRacedId, payoutType), () =>
            {
                return (from rlrb in dataContext.ReportLeagueRaceBets
                        where rlrb.RaceDetail.LeagueRace.Id == leagueRacedId && rlrb.BetType == Convert.ToInt32(payoutType)
                        orderby rlrb.UserBetCount descending
                        select rlrb).ToList();
            });
        }

        public void PersistUserPicks(IList<UserRaceDetail> userPicks, int leagueRaceId, System.Guid userId)
        {
            //Delete 
            dataContext.ExecuteCommand(String.Format("DELETE UserRaceDetail FROM UserRaceDetail INNER JOIN RaceDetails ON RaceDetails.RaceDetailId = UserRaceDetail.RaceDetailId WHERE RaceDetails.LeagueRaceId={0} AND UserId=CONVERT(uniqueidentifier,'{1}')",leagueRaceId, userId));
            
            //Insert
            foreach (UserRaceDetail urd in userPicks)
            {
                dataContext.ExecuteCommand(String.Format("INSERT INTO UserRaceDetail (RaceDetailId, LeagueRaceId, UserId, UpdateDate, BetType) VALUES( {0}, {1}, CONVERT(uniqueidentifier, '{2}'), '{3}', '{4}')", urd.RaceDetailId, urd.LeagueRaceId, userId, urd.UpdateDate, urd.BetType ));
            }

            OnPicksSet(this, new UserPicksEventArgs(userId, leagueRaceId));
        }

        public void PersistLeagueRace(LeagueRaceDomain leagueRace)
        {
            dataContext.ExecuteCommand(String.Format("UPDATE LeagueRace SET PostTime='{0}', IsActive={1}, FormUrl='{2}' WHERE Id={3}", 
                leagueRace.PostTime, leagueRace.IsActive, leagueRace.FormUrl,leagueRace.Id));

            OnLeagueRaceChange(this, new LeagueRaceEventArgs(leagueRace.Id));
        }

        public aspnet_User GetUser(System.Guid userId)
        {
            return (from user in dataContext.aspnet_Users
                        where user.UserId == userId
                        select user).FirstOrDefault();
        }

        public Horse GetHorse(string horseName)
        {
            return (from hs in dataContext.Horses
                    where hs.Name.ToLower() == horseName.ToLower()
                    select hs).FirstOrDefault();
        }

        public RaceDetail GetRaceDetail(int raceDetailId)
        {
            return (from rd in dataContext.RaceDetails
                    where rd.RaceDetailId == raceDetailId
                    select rd).FirstOrDefault();
        }

        public RaceDetailPayout GetPayout(LeagueRace leagueRace, BetTypes payoutType)
        {
            return (from rd in dataContext.RaceDetailPayouts
                    where (rd.BetType == Convert.ToInt32(payoutType)) && (rd.LeagueRaceId == leagueRace.Id)
                    select rd).FirstOrDefault();
        }


        public Horse PersistHorse(Horse horse)
        {
            dataContext.Horses.InsertOnSubmit(horse);
            dataContext.SubmitChanges();

            return horse;
        }

        public RaceDetail PersistRaceDetail(RaceDetail raceDetail)
        {
            dataContext.RaceDetails.InsertOnSubmit(raceDetail);
            dataContext.SubmitChanges();
 
            OnRaceDetailChange(this, new RaceDetailEventArgs(raceDetail));

            return raceDetail;
        }

        public RaceDetail UpdateRaceDetail(RaceDetail raceDetail)
        {
            dataContext.ExecuteCommand(String.Format("UPDATE RaceDetails SET PostPosition={0}, IsScratched={1}, OddsOrder={2} WHERE RaceDetailId={3}", 
                raceDetail.PostPosition, raceDetail.IsScratched, raceDetail.OddsOrder, raceDetail.RaceDetailId));

            RaceDetail rd = GetRaceDetail(raceDetail.RaceDetailId);

            OnRaceDetailChange(this, new RaceDetailEventArgs(rd));

            return rd;
        }

        public void PersistRaceDetailPayout(RaceDetailPayout racePayout)
        {
            dataContext.RaceDetailPayouts.InsertOnSubmit(racePayout);
            dataContext.SubmitChanges();

            OnLeagueRaceChange(this, new LeagueRaceEventArgs(racePayout.LeagueRaceId));
        }

        public void RecalculateStandings()
        {
            dataContext.ExecuteCommand("EXEC CalculateUserTotals");

            OnStandingsRecalculated(this, new UserStandingsEventArgs());
        }

        void HorseLeagueRepository_OnPicksSet(object sender, UserPicksEventArgs e)
        {
            //Refresh the dataContext
            dataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, dataContext.UserRaceDetails);

            //Remove all cached races
            this.cache.Remove(ActiveRacesCacheKey.Create());
        }

        void HorseLeagueRepository_OnLeagueRaceChange(object sender, LeagueRaceEventArgs e)
        {
            this.cache.Remove(LeagueRaceCacheKey.Create(e.LeagueRaceId));
        }
        #endregion

        void HorseLeagueRepository_OnRaceDetailChange(object sender, RaceDetailEventArgs e)
        {
            this.cache.Remove(LeagueRaceCacheKey.Create(e.RaceDetail.LeagueRace.Id));
            this.cache.Remove(ResultsCacheKey.Create());
            this.cache.Remove(ActiveRacesCacheKey.Create());
            this.cache.Remove(LeagueRaceScratchesCacheKey.Create(e.RaceDetail.LeagueRaceId));
        }

        void HorseLeagueRepository_OnStandingsRecalculated(object sender, UserStandingsEventArgs e)
        {
            this.cache.Remove(UserStandingsCacheKey.Create()) ;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }

        

        #endregion
    }
}
