using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class RaceExoticPayoutMap : ClassMap<RaceExoticPayout>
    {
        public RaceExoticPayoutMap()
        {
            Id(x => x.Id)
                .Column("RaceExoticPayoutId");

            Map(x => x.BetType, "BetType").CustomType<BetTypes>();
            Map(x => x.Amount, "Amount");

            References<LeagueRace>(x => x.LeagueRace)
                .Column("LeagueRaceId")
                .Cascade.None();
        }
    }
}
  