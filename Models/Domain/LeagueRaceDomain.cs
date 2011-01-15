using System;
using System.Collections.Generic;
using System.Linq;

using HorseLeague.Models.DataAccess;


namespace HorseLeague.Models
{
    public class LeagueRaceDomain
    {
        private readonly LeagueRace _leagueRace;
        private readonly IHorseLeagueRepository _repository;

        public LeagueRaceDomain(LeagueRace leagueRace) : this(leagueRace, new HorseLeagueRepository()) {}

        public LeagueRaceDomain(LeagueRace leagueRace, IHorseLeagueRepository repository)
        {
            this._leagueRace = leagueRace;
            this._repository = repository;
        }

        public IList<RaceDetail> GetScratches()
        {
            return this._repository.GetScratches(this.Id);
        }

        public RaceDetailPayout GetPayout(BetTypes payoutType)
        {
            return this._leagueRace.RaceDetailPayouts.Where(x => x.BetType == Convert.ToInt32(payoutType)).FirstOrDefault();
            //return this._repository.GetPayout(this._leagueRace, payoutType);
        }

        public DateTime PostTimeEST
        {
            get
            {
                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                // Convert EST to local time
                return TimeZoneInfo.ConvertTime(PostTimeUTC, TimeZoneInfo.Utc, est);
            }
            set
            {
                PostTimeUTC = value.ToUniversalTime();
            }
        }

        public DateTime PostTimeUTC
        {
            get
            {
                if(this._leagueRace.PostTime == null)
                    return DateTime.MinValue;
                else
                    return (DateTime)this._leagueRace.PostTime;
            }
            set
            {
                DateTime result;

                if (DateTime.TryParse(value.ToString(), out result))
                {
                    _leagueRace.PostTime = result;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("PostTime", value, "The post time was not a valid date time.");
                }
            }
        }

        public int IsActive
        {
            get
            {
                return _leagueRace.IsActive;
            }
            set
            {
                _leagueRace.IsActive = value;
            }
        }

        public int Id
        {
            get
            {
                return _leagueRace.Id;
            }
        }

        public string FormUrl
        {
            get
            {
                return _leagueRace.FormUrl;
            }
            set
            {
                _leagueRace.FormUrl = value;
            }
        }

        public int Weight
        {
            get
            {
                return _leagueRace.Weight;
            }
        }

        public Race Race
        {
            get
            {
                return _leagueRace.Race;
            }
        }

        public bool HasCompletePayoutInfo
        {
            get
            {
                return this._leagueRace.RaceDetailPayouts.Count == 3;
            }
        }

        public IList<RaceDetail> RaceDetails
        {
            get
            {
                return _leagueRace.RaceDetails.OrderBy(raceDetail => raceDetail.PostPosition).ToList();
            }
        }

        public IList<RaceDetail> RaceDetailsByOdds
        {
            get
            {
                return _leagueRace.RaceDetails.OrderBy(raceDetail => raceDetail.OddsOrder).ToList();
            }
        }

        public RaceDetail Favorite
        {
            get
            {
                return RaceDetailsByOdds.Single(raceDetail => raceDetail.OddsOrder == 1) ;
            }
        }

        public RaceDetailPayout Win
        {
            get
            {
                return this.GetPayout(BetTypes.Win);
            }
        }

        public RaceDetailPayout Place
        {
            get
            {
                return this.GetPayout(BetTypes.Place);
            }
        }

        public RaceDetailPayout Show
        {
            get
            {
                return this.GetPayout(BetTypes.Show);
            }
        }

        public RaceDetailPayout GetFinishPosition(RaceDetail raceDetail)
        {
            if(Win.RaceDetailId == raceDetail.RaceDetailId)
                return Win;

            if(Place.RaceDetailId == raceDetail.RaceDetailId)
                return Place;

            if(Show.RaceDetailId == raceDetail.RaceDetailId)
                return Show;

            return null;  
        }
    }
}
