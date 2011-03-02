using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class RaceExoticPayout : Entity
    {
        public virtual LeagueRace LeagueRace { get; set; }
        public virtual BetTypes BetType { get; set; }
        public virtual float Amount { get; set; }
    }
}