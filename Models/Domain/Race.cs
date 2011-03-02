using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class Race : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Track { get; set; }
    }
}