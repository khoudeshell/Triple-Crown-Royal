using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;
using System.Text;

namespace HorseLeague.Models.Domain
{
    public class UserLeague : Entity, IEmailable
    {
        public virtual IList<UserRaceDetail> UserRaceDetails { get; set; }
        public virtual User User { get; set; }
        public virtual League League { get; set; }

        public virtual bool HasValidRaceCondition(LeagueRace leagueRace)
        {
            return GetPicksForARace(leagueRace).Count > 0;
        }

        public virtual bool IsValidRaceCondition(LeagueRace leagueRace)
        {
            var disPicks = (from dp in GetPicksForARaceQueryable(leagueRace)
                            select dp.RaceDetail.Id).Distinct();

            return disPicks.ToList().Count == 4; //There are 4 possible user entered bet types
        }

        public virtual void ClearUserPicks(LeagueRace leagueRace)
        {
            IList<UserRaceDetail> picks = this.GetPicksForARace(leagueRace);
            foreach (UserRaceDetail urd in picks)
            {
                this.UserRaceDetails.Remove(urd);
            }
            
        }

        public virtual void AddUserPick(LeagueRace leagueRace, RaceDetail raceDetail, BetTypes betType)
        {
            var userPick = GetPicksForARace(leagueRace).Where(x => x.BetType == betType).FirstOrDefault();

            if (userPick != null && userPick.RaceDetail != raceDetail)
            {
                this.UserRaceDetails.Remove(userPick);
            }
            else if(userPick != null && userPick.RaceDetail == raceDetail)
            {
                return;
            }
            createAndAddUserRace(betType, raceDetail);
        }

        public virtual bool HasUserSetPicksForRace(LeagueRace leagueRace)
        {
            return (from urd in UserRaceDetails
                    where urd.RaceDetail.LeagueRace == leagueRace
                    select urd).Count() > 0;
        }

        public virtual UserRaceDetail GetUserPickByType(LeagueRace leagueRace, BetTypes betType)
        {
            return this.GetPicksForARace(leagueRace).Where(x => x.BetType == betType).FirstOrDefault();
        }

        
        

        private IQueryable<UserRaceDetail> GetPicksForARaceQueryable(LeagueRace leagueRace)
        {
            return UserRaceDetails.Where(x => x.RaceDetail.LeagueRace == leagueRace).AsQueryable<UserRaceDetail>();
        }

        public virtual IList<UserRaceDetail> GetPicksForARace(LeagueRace leagueRace)
        {
            return GetPicksForARaceQueryable(leagueRace).ToList<UserRaceDetail>();
        }

        public virtual bool WasExactaWinner(LeagueRace leagueRace)
        {
            RaceDetailPayout winner = leagueRace.Win;
            RaceDetailPayout place = leagueRace.Place;

            IList<UserRaceDetail> userRaceDetail = GetPicksForARace(leagueRace);
            if(!this.HasValidRaceCondition(leagueRace))
                return false;

            return (winner.RaceDetail == userRaceDetail.Where(x => x.BetType == BetTypes.Win).FirstOrDefault().RaceDetail
                && place.RaceDetail == userRaceDetail.Where(x => x.BetType == BetTypes.Place).FirstOrDefault().RaceDetail);
        }

        public virtual bool WasTrifectaWinner(LeagueRace leagueRace)
        {
            RaceDetailPayout show = leagueRace.Show;
            IList<UserRaceDetail> userRaceDetail = GetPicksForARace(leagueRace);
            
            return WasExactaWinner(leagueRace) && 
                (show.RaceDetail == userRaceDetail.Where(x => x.BetType == 
                    BetTypes.Show).FirstOrDefault().RaceDetail);
        }

        private void createAndAddUserRace(BetTypes betType, RaceDetail raceDetail)
        {
            UserRaceDetail urd = new UserRaceDetail();

            urd.BetType = betType;
            urd.RaceDetail = raceDetail;
            urd.UserLeague = this;
            urd.UpdateDate = System.DateTime.Now;

            this.UserRaceDetails.Add(urd);
        }

        #region IEmailable Members

        public virtual string GetSubject(LeagueRace leagueRace)
        {
            return String.Format("Picks for {0}", leagueRace.Race.Name); 
        }

        public virtual string GetBody(LeagueRace leagueRace)
        {
            StringBuilder emailBody = new StringBuilder();

            CreatePickText(leagueRace, emailBody, BetTypes.Win);
            CreatePickText(leagueRace, emailBody, BetTypes.Place);
            CreatePickText(leagueRace, emailBody, BetTypes.Show);
            CreatePickText(leagueRace, emailBody, BetTypes.Backup);

            emailBody.AppendLine("");
            emailBody.AppendLine("User id: " + this.User.Id.ToString());

            return emailBody.ToString();
            
        }

        private void CreatePickText(LeagueRace leagueRace, StringBuilder output, BetTypes betType)
        {
            RaceDetail rd = this.GetUserPickByType(leagueRace, betType).RaceDetail;

            output.AppendLine(String.Format("{0}: {1}-{2}", betType.ToString(),
                rd.PostPosition, rd.Horse.Name));
        }
        #endregion
    }
}