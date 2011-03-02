using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class RaceDetail : Entity
    {
        public virtual int PostPosition { get; set; }
        public virtual int IsScratched { get; set; }
        public virtual int OddsOrder { get; set; }

        public virtual Horse Horse { get; set; }
        public virtual LeagueRace LeagueRace { get; set; }
        public virtual IList<RaceDetailPayout> RaceDetailPayout { get; set; }

        public virtual RaceDetailPayout GetRaceDetailPayout()
        {
            if (RaceDetailPayout == null || RaceDetailPayout.Count == 0)
                return null;

            return RaceDetailPayout[0];
        }
    }
}