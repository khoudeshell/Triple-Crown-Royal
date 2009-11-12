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
using System.Collections.Generic;

namespace HorseLeague.Models
{
    public class UserLeagueRacePicksDomain
    {
        private readonly IHorseLeagueRepository _repository;
        private readonly System.Guid _userId;
        private readonly int _leagueRaceId;

        private LeagueRaceDomain _leagueRace;
        private IList<UserRaceDetail> _userPicks;

        public UserLeagueRacePicksDomain(Guid userId, int leagueRaceId, IHorseLeagueRepository repository)
        {
            _userId = userId;
            _repository = repository;
            _leagueRaceId = leagueRaceId;
        }

        public LeagueRaceDomain LeagueRace
        {
            get
            {
                if (_leagueRace == null)
                {
                    _leagueRace = new LeagueRaceDomain(this._repository.GetLeagueRace(_leagueRaceId), this._repository);
                }

                return _leagueRace;
            }
        }

        public Guid UserId
        {
            get
            {
                return _userId;
            }
        }

        public IList<UserRaceDetail> UserPicks
        {
            get
            {
                if (_userPicks == null)
                {
                    _userPicks = this._repository.GetUserPicks(this._leagueRaceId, this._userId);
                }

                return _userPicks;
            }
        }

        public bool IsValidRaceCondition
        {
            get
            {
                var disPicks = (from dp in this._userPicks
                                select dp.RaceDetailId).Distinct();

                return disPicks.ToList().Count == _userPicks.Count;
            }
        }

        public virtual bool IsUpdateable
        {
            get
            {
                return DateTime.Now.ToUniversalTime().Ticks < Convert.ToDateTime(this.LeagueRace.PostTime).ToUniversalTime().Ticks;
            }
        }

        public void AddUserPick(int raceDetailId, BetTypes betType)
        {
            var userPick = (from up in _userPicks where up.BetType == Convert.ToInt32(betType)
                       select up).FirstOrDefault();

            if(userPick != null)
            {
                throw new InvalidOperationException(String.Format("A pick for the {0} bet type has already been added.", betType.ToString()));
            }
            
            createAndAddUserRace(betType, this.UserId, raceDetailId, this._leagueRaceId, this._userPicks);
        }

        public void UpdatePicks()
        {
            this._repository.PersistUserPicks(this.UserPicks, this._leagueRaceId, this._userId);
        }

        private void createAndAddUserRace(BetTypes betType, System.Guid userId, int raceDetailId,
            int leagueRaceId, IList<UserRaceDetail> userRaceDetails)
        {
            UserRaceDetail urd = new UserRaceDetail();

            urd.BetType = Convert.ToInt32(betType);
            urd.RaceDetailId = raceDetailId;
            urd.UserId = userId;
            urd.UpdateDate = System.DateTime.Now;
            urd.LeagueRaceId = leagueRaceId;
            userRaceDetails.Add(urd);
        }
    }
}
