using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseLeague.Models.Domain
{
    public interface IEmailable
    {
        string GetSubject(LeagueRace leagueRace);
        string GetBody(LeagueRace leagueRace);
    }
}
