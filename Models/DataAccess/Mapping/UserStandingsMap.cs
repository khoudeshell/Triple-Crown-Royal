using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorseLeague.Models.Domain;
using FluentNHibernate.Mapping;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class UserStandingsMap : ClassMap<UserStandings>
    {
        public UserStandingsMap()
        {
            Table("UserStandings");
            Id(x => x.Id);

            Map(x => x.Year, "Yr");
            Map(x => x.Total, "Total");
            Map(x => x.UpdateDate, "UpdateDate");
            Map(x => x.WinWinPct, "WinWinPct");
            Map(x => x.WinPlacePct, "WinPlacePct");
            Map(x => x.WinShowPct, "WinShowPct");
            Map(x => x.PlacePlacePct, "PlacePlacePct");
            Map(x => x.PlaceShowPct, "PlaceShowPct");
	        Map(x => x.ShowShowPct, "ShowShowPct");
	        Map(x => x.WinWinAvg, "WinWinAvg");
	        Map(x => x.WinPlaceAvg, "WinPlaceAvg");
	        Map(x => x.WinShowAvg, "WinShowAvg");
	        Map(x => x.PlacePlaceAvg, "PlacePlaceAvg");
	        Map(x => x.PlaceShowAvg, "PlaceShowAvg");
	        Map(x => x.ShowShowAvg, "ShowShowAvg");
	        Map(x => x.WinFavPct, "WinFavPct");
	        Map(x => x.ROI, "ROI");
	    
            References<UserLeague>(x => x.UserLeague)
                .Column("UserLeagueId")
                .Fetch.Join()
                .Cascade.None();

            References<League>(x => x.League)
                .Column("LeagueId")
                .Cascade.None();
        }
    }
}