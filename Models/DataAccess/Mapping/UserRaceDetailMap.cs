using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorseLeague.Models.Domain;
using FluentNHibernate.Mapping;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class UserRaceDetailMap : ClassMap<UserRaceDetail>
    {
        public UserRaceDetailMap()
        {
            Table("UserRaceDetail");
            Id(x => x.Id, "UserRaceDetailId");
            Map(x => x.BetType, "BetType").CustomType<BetTypes>();
            Map(x => x.UpdateDate, "UpdateDate");

            References<UserLeague>(x => x.UserLeague)
                .Column("UserLeagueId")
                .Cascade.None();

            References<RaceDetail>(x => x.RaceDetail)
                .Column("RaceDetailId")
                .Fetch.Join()
                .Cascade.None();

        }
    }
}