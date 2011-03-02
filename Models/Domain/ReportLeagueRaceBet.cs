using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class ReportLeagueRaceBet : Entity
    {
        public virtual RaceDetail RaceDetail { get; set; }
        public virtual LeagueRace LeagueRace { get; set; }
        public virtual BetTypes BetTypes { get; set; }
        public virtual int UserBetCount { get; set; }
    }
}