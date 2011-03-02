using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class LeagueMap : ClassMap<League>
    {
        public LeagueMap()
        {
            Table("League");

            Id(x => x.Id, "LeagueId");

            HasMany<LeagueRace>(x => x.LeagueRaces)
                .KeyColumn("LeagueId")
                .Cascade.None();

            HasMany<UserStandings>(x => x.UserStandings)
                .KeyColumn("LeagueId")
                .AsBag()
                .Cascade.None();

            HasMany<UserRaceExoticPayout>(x => x.UserRaceExoticPayouts)
               .KeyColumn("LeagueId")
               .Inverse()
               .Cascade.AllDeleteOrphan()
               .AsBag();
        }
    }
}