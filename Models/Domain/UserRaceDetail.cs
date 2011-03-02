using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class UserRaceDetail : Entity
    {
        public virtual RaceDetail RaceDetail { get; set; }
        public virtual UserLeague UserLeague { get; set; }
        public virtual BetTypes BetType { get; set; }
        public virtual DateTime UpdateDate { get; set; }
    }
}