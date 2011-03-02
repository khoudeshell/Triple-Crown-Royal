using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class League : Entity
    {
        public virtual IList<LeagueRace> LeagueRaces { get; set; }
        public virtual IList<UserStandings> UserStandings { get; set; }
        public virtual IList<UserRaceExoticPayout> UserRaceExoticPayouts { get; set; }

        public virtual LeagueRace GetLeagueRace(int leagueRaceId)
        {
            return LeagueRaces.Where(x => x.Id == leagueRaceId).FirstOrDefault();
        }

        public virtual IList<LeagueRace> ActiveRaces 
        {
            get
            {
                return LeagueRaces.Where(x => x.IsActive == 1).ToList<LeagueRace>();
            }
        }

        public virtual IList<UserRaceExoticPayout> GetExactaPayouts()
        {
            return GetExoticPayout(BetTypes.Exacta);
        }

        public virtual IList<UserRaceExoticPayout> GetTrifectaPayouts()
        {
            return GetExoticPayout(BetTypes.Trifecta);
        }

        private IList<UserRaceExoticPayout> GetExoticPayout(BetTypes betType)
        {
            return this.UserRaceExoticPayouts
                .Where(x => x.RaceExoticPayout.BetType == betType)
                .OrderByDescending(x => x.RaceExoticPayout.Amount)
                .Take(5)
                .ToList<UserRaceExoticPayout>();
        }
    }
}