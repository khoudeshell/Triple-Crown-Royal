using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class RaceDetailMap : ClassMap<RaceDetail>
    {
        public RaceDetailMap()
        {
            Table("RaceDetails");
            Id(x => x.Id, "RaceDetailId");

            Map(x => x.PostPosition, "PostPosition");
            Map(x => x.IsScratched, "IsScratched");
            Map(x => x.OddsOrder, "OddsOrder");

            References<Horse>(x => x.Horse)
                .Column("HorseId")
                .ForeignKey("HorseId")
                .Fetch.Join()
                .Cascade.None();

            References<LeagueRace>(x => x.LeagueRace)
                .Column("LeagueRaceId")
                .ForeignKey("LeagueRaceId")
                .Cascade.None();

            HasMany<RaceDetailPayout>(x => x.RaceDetailPayout)
                .KeyColumn("RaceDetailId")
                .Fetch.Join()
                .LazyLoad()
                .Inverse()
                .Cascade.All();
        }
    }
}