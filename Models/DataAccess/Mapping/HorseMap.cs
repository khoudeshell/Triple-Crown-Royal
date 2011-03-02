using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class HorseMap : ClassMap<Horse>
    {
        public HorseMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}