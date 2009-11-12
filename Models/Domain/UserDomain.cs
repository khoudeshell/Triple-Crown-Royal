using System;
using System.Collections.Generic;
using System.Linq;

using HorseLeague.Models.DataAccess;

namespace HorseLeague.Models
{
    public class UserDomain
    {
        private aspnet_User _user;

        public UserDomain(aspnet_User user)
        {
            this._user = user;
        }

        public bool HasUserSetPicksForRace(LeagueRace leagueRace)
        {
            return (from urd in _user.UserRaceDetails
                    where urd.LeagueRaceId == leagueRace.Id
                    select urd).Count() > 0;
        }
    }
}
