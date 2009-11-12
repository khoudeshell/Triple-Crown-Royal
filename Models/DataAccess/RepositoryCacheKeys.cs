using System;
using System.Web.Caching;

using HorseLeague.Cache;

namespace HorseLeague.Models.DataAccess
{
    public class ActiveRacesCacheKey : CacheKey
    {
        private ActiveRacesCacheKey() : base("HorseLeague:ActiveRaces", null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create()
        {
            return new ActiveRacesCacheKey();
        }
    }

    public class AllRacesCacheKey : CacheKey
    {
        private AllRacesCacheKey() : base("HorseLeague:AllRaces", null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create()
        {
            return new AllRacesCacheKey();
        }
    }

    public class LeagueRaceCacheKey : CacheKey
    {
        private LeagueRaceCacheKey(int leagueRaceId) : base(String.Format("HorseLeague:LeagueRace:{0}", leagueRaceId.ToString()) , null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create(int leagueRaceId)
        {
            return new LeagueRaceCacheKey(leagueRaceId);
        }
    }

    public class LeagueRaceScratchesCacheKey : CacheKey
    {
        private LeagueRaceScratchesCacheKey(int leagueRaceId) : base(String.Format("HorseLeague:LeagueRaceScratchesCacheKey:{0}", leagueRaceId.ToString()), null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create(int leagueRaceId)
        {
            return new LeagueRaceScratchesCacheKey(leagueRaceId);
        }
    }

    public class LeagueRaceBetTypeReportCacheKey : CacheKey
    {
        private LeagueRaceBetTypeReportCacheKey(int leagueRaceId, BetTypes payoutType) : base(String.Format("HorseLeague:LeagueRaceBetTypeReport:{0}{1}", leagueRaceId.ToString(), Convert.ToInt32(payoutType).ToString()), null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create(int leagueRaceId, BetTypes payoutType)
        {
            return new LeagueRaceBetTypeReportCacheKey(leagueRaceId, payoutType);
        }
    }

    public class ResultsCacheKey : CacheKey
    {
        private ResultsCacheKey() : base("HorseLeague:Results", null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create()
        {
            return new ResultsCacheKey();
        }
    }

    public class UserStandingsCacheKey : CacheKey
    {
        private UserStandingsCacheKey() : base("HorseLeague:UserStandings", null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create()
        {
            return new UserStandingsCacheKey();
        }
    }

    public class UserResultsCacheKey : CacheKey
    {
        private UserResultsCacheKey(System.Guid userId) : base(String.Format("HorseLeague:UserResults:{0}", userId), null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create(System.Guid userId)
        {
            return new UserResultsCacheKey(userId);
        }
    }

    public class AllUsersCacheKey : CacheKey
    {
        private AllUsersCacheKey() : base("HorseLeague:AllUsers", null, DateTime.Now.AddMinutes(20)) { }

        public static CacheKey Create()
        {
            return new AllUsersCacheKey();
        }
    }
}
