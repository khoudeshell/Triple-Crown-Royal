using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class UserRaceExoticPayoutMap : ClassMap<UserRaceExoticPayout>
    {
        public UserRaceExoticPayoutMap()
        {
            Table("UserLeagueRaceExoticPayout");
            Id(x => x.Id, "UserLeagueRaceExoticPayoutId");

            References<RaceExoticPayout>(x => x.RaceExoticPayout)
               .Column("RaceExoticPayoutId")
               .ForeignKey("RaceExoticPayoutId")
               .Fetch.Join()
               .Cascade.None();

            References<UserLeague>(x => x.UserLeague)
               .Column("UserLeagueId")
               .Cascade.None();

            References<League>(x => x.League)
                .Column("LeagueId")
                .Cascade.None();
        }
    }
}