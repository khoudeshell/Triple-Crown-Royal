using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class ReportLeagueRaceBetMap : ClassMap<ReportLeagueRaceBet>
    {
        public ReportLeagueRaceBetMap()
        {
            Table("ReportLeagueRaceBet");
            Id(x => x.Id, "ReportLeagueRaceBetId");

            Map(x => x.UserBetCount, "UserBetCount");
            Map(x => x.BetTypes, "BetType").CustomType<BetTypes>();
           
            References<RaceDetail>(x => x.RaceDetail)
                .Column("RaceDetailId")
                .Cascade.None();

            References<LeagueRace>(x => x.LeagueRace)
                .Column("LeagueRaceId")           
                .Cascade.None();
        }
    }
}