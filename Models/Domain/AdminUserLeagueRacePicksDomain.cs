using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HorseLeague.Models.DataAccess;

namespace HorseLeague.Models
{
    public class AdminUserLeagueRacePicksDomain : UserLeagueRacePicksDomain
    {
        public AdminUserLeagueRacePicksDomain(Guid userId, int leagueRaceId, IHorseLeagueRepository repository) : base(userId, leagueRaceId, repository) { }

        public override bool IsUpdateable
        {
            get
            {
                return true;
            }
        }
    }
}
