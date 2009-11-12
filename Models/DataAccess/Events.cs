using System;


namespace HorseLeague.Models.DataAccess
{
    public class UserPicksEventArgs : EventArgs
    {
        private readonly Guid _userId;
        private readonly int _leagueRaceId;

        public UserPicksEventArgs(Guid userId, int leagueRaceId)
        {
            this._userId = userId;
            this._leagueRaceId = leagueRaceId;
        }

        public Guid UserId
        {
            get
            {
                return _userId;
            }
        }

        public int LeagueRaceId
        {
            get
            {
                return _leagueRaceId;
            }
        }
    }

    public class LeagueRaceEventArgs : EventArgs
    {
        private readonly int _leagueRaceId;

        public LeagueRaceEventArgs(int leagueRaceId)
        {
            _leagueRaceId = leagueRaceId;
        }

        public int LeagueRaceId
        {
            get
            {
                return _leagueRaceId;
            }
        }
    }

    public class RaceDetailEventArgs : EventArgs
    {
        private readonly RaceDetail _raceDetail;

        public RaceDetailEventArgs(RaceDetail raceDetail)
        {
            _raceDetail = raceDetail;
        }

        public RaceDetail RaceDetail
        {
            get
            {
                return _raceDetail;
            }
        }
    }

    public class UserStandingsEventArgs : EventArgs { }

}
