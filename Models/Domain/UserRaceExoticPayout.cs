using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class UserRaceExoticPayout : Entity
    {
        public virtual RaceExoticPayout RaceExoticPayout { get; set; }
        public virtual UserLeague UserLeague { get; set; }
        public virtual League League { get; set; }
    }
}