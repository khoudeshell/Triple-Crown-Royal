using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class RaceDetailPayoutMap : ClassMap<RaceDetailPayout>
    {
        public RaceDetailPayoutMap()
        {
            Table("RaceDetailPayout");
            Id(x => x.Id, "RaceDetailPayoutId");

            Map(x => x.BetType, "BetType").CustomType<BetTypes>();
            Map(x => x.WinAmount, "WinAmount");
            Map(x => x.PlaceAmount, "PlaceAmount");
            Map(x => x.ShowAmount, "ShowAmount");
        
            References<RaceDetail>(x => x.RaceDetail)
                .Column("RaceDetailId")
                .Fetch.Join()
                .Cascade.None();

            References<LeagueRace>(x => x.LeagueRace)
                .Column("LeagueRaceId")
                .Cascade.None();
        }
    }
}