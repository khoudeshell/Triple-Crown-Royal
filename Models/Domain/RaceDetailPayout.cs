using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class RaceDetailPayout : Entity
    {
        public virtual RaceDetail RaceDetail { get; set; }
        public virtual LeagueRace LeagueRace { get; set; }
        public virtual BetTypes BetType { get; set; }
        public virtual float? WinAmount { get; set; }
        public virtual float? PlaceAmount { get; set; }
        public virtual float? ShowAmount { get; set; }
    }
}