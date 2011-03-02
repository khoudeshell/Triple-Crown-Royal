using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class UserLeagueMap : ClassMap<UserLeague>
    {
        public UserLeagueMap()
        {
            Table("UserLeague");
            Id(x => x.Id, "UserLeagueId");

            References<User>(x => x.User)
                .Column("UserId")
                .Fetch.Join()
                .Cascade.None();

            References<League>(x => x.League)
                .Column("LeagueId")
                .Cascade.None();

            HasMany<UserRaceDetail>(x => x.UserRaceDetails)
                .KeyColumn("UserLeagueId")
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .AsBag();           
        }
    }
}