using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class LeagueRaceMap : ClassMap<LeagueRace>
    {
        public LeagueRaceMap()
        {
            Table("LeagueRace");

            Id(x => x.Id, "Id");

            Map(x => x.RaceDate, "Dt");
            Map(x => x.Weight, "Weight");
            Map(x => x.PostTime, "PostTime");
            Map(x => x.IsActive, "IsActive");
            Map(x => x.FormUrl, "FormUrl");

            References<Race>(x => x.Race)
                .Column("RaceId")
                .Fetch.Join()
                .Cascade.None();

            References<League>(x => x.League)
                .Column("LeagueId")
                .Cascade.None();

            HasMany<RaceDetail>(x => x.RaceDetails)
                .KeyColumn("LeagueRaceId")
                .Inverse()
                .Cascade.All();

            HasMany<RaceExoticPayout>(x => x.RaceExoticPayouts)
                .KeyColumn("LeagueRaceId")
                .Cascade.All();

            HasMany<RaceDetailPayout>(x => x.RaceDetailPayouts)
                .KeyColumn("LeagueRaceId")
                .Inverse()
                .Fetch.Join()
                .LazyLoad()
                .Cascade.All();
       
            HasMany<ReportLeagueRaceBet>(x => x.ReportLeagueRaceBets)
                .KeyColumn("LeagueRaceId")
                .Cascade.None();
       
        }
    }
}