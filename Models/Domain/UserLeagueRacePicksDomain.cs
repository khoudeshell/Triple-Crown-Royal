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
using System.Text;
using System.Web.Mail;
using HorseLeague.Models.Domain;

namespace HorseLeague.Models
{
    public class UserLeagueRacePicksDomain : IEmailable
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
                return DateTime.UtcNow < this.LeagueRace.PostTimeUTC;
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

        

        private RaceDetail GetUserPickByType(BetTypes betType)
        {
            //return this._repository.GetRaceDetail(this.UserPicks.Where(x => 
            //    x.BetType == Convert.ToInt32(betType)).Single().RaceDetailId);

            return this.LeagueRace.RaceDetails.Where(rd => rd.RaceDetailId == (this.UserPicks.Where(x => 
                x.BetType == Convert.ToInt32(betType)).Single().RaceDetailId)).FirstOrDefault();
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

        #region IEmailable Members

        public string Subject
        {
            get { return String.Format("Picks for {0}", this.LeagueRace.Race.Name); }
        }

        public string Body
        {
            get 
            { 
                StringBuilder emailBody = new StringBuilder();

                CreatePickText(emailBody, BetTypes.Win);
                CreatePickText(emailBody, BetTypes.Place);
                CreatePickText(emailBody, BetTypes.Show);
                CreatePickText(emailBody, BetTypes.Backup);

                emailBody.AppendLine("");
                emailBody.AppendLine("User id: " + this.UserId.ToString());

                return emailBody.ToString();
            }
        }

        private void CreatePickText(StringBuilder output, BetTypes betType)
        {
            RaceDetail rd = GetUserPickByType(betType);

            output.AppendLine(String.Format("{0}: {1}-{2}", betType.ToString(),
                rd.PostPosition, rd.Horse.Name));
        }
        #endregion
    }
}
