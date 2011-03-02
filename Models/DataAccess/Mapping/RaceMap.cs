using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorseLeague.Models.Domain;
using FluentNHibernate.Mapping;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class RaceMap : ClassMap<Race>
    {
        public RaceMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Track);
            
            Cache.ReadOnly();
        }

         
    }
}