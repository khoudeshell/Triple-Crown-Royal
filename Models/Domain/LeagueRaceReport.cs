using System;
using System.Collections.Generic;
using System.Linq;

using HorseLeague.Models.DataAccess;


namespace HorseLeague.Models
{
    public class LeagueRaceReport
    {
        private IList<ReportLeagueRaceBet> _win;
        private IList<ReportLeagueRaceBet> _place;
        private IList<ReportLeagueRaceBet> _show;

        public LeagueRaceReport(IList<ReportLeagueRaceBet> results)
        {
            _win = results.Where(x => x.BetType == Convert.ToInt32(HorseLeague.Models.BetTypes.Win)).ToList();
            _place = results.Where(x => x.BetType == Convert.ToInt32(HorseLeague.Models.BetTypes.Place)).ToList();
            _show = results.Where(x => x.BetType == Convert.ToInt32(HorseLeague.Models.BetTypes.Show)).ToList();
        }

        public IList<ReportLeagueRaceBet> Win
        {
            get { return _win; }
        }

        public IList<ReportLeagueRaceBet> Place
        {
            get { return _place; }
        }

        public IList<ReportLeagueRaceBet> Show
        {
            get { return _show; }
        }

        public IList<ReportLeagueRaceBet> GetListByType(BetTypes betType)
        {
            switch(betType)
            {
                case BetTypes.Win:
                    return Win;
                case BetTypes.Place:
                    return Place;
                case BetTypes.Show:
                    return Show;
                default:
                    return null;
            }
        }
    }
}
