using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models.DataAccess.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("aspnet_users");
            Id(x => x.Id, "UserId");

            Map(x => x.UserName, "UserName");
            HasMany<UserLeague>(x => x.UserLeagues)
                .KeyColumn("UserId")
                .Fetch.Join()
                .Cascade.AllDeleteOrphan();
        }
    }
}