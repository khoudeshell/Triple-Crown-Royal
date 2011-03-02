using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Data;

namespace HorseLeague.Models.Domain
{
    public class Horse : Entity
    {
        public virtual string Name { get; set; }
    }
}