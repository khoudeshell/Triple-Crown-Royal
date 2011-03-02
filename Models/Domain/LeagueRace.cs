using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class LeagueRace : Entity
    {
        public virtual DateTime RaceDate { get; set; }
        public virtual int Weight { get; set; }
        public virtual DateTime? PostTime { get; set; }
        public virtual int IsActive { get; set; }
        public virtual string FormUrl { get; set; }

        public virtual Race Race { get; set; }
        public virtual League League { get; set; }

        public virtual IList<RaceDetail> RaceDetails { get; set; }
        public virtual IList<ReportLeagueRaceBet> ReportLeagueRaceBets { get; set; }
        public virtual IList<RaceDetailPayout> RaceDetailPayouts { get; set; }
        public virtual IList<RaceExoticPayout> RaceExoticPayouts { get; set; }

        private bool updateOverride = false;
        private bool isUpdateable = false;

        public virtual bool IsUpdateable
        {
            get
            {
                if (updateOverride)
                    return isUpdateable;

                return DateTime.UtcNow < this.PostTimeUTC;
            }
            set
            {
                updateOverride = true;
                isUpdateable = value;
            }
        }

        public virtual IList<RaceDetail> GetScratches()
        {
            return this.RaceDetails.Where(x => x.IsScratched == 1).ToList<RaceDetail>();
        }

        public virtual RaceDetailPayout GetPayout(BetTypes payoutType)
        {
            return this.RaceDetailPayouts.Where(x => x.BetType == payoutType).FirstOrDefault();
        }

        public virtual DateTime PostTimeEST
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

        public virtual DateTime PostTimeUTC
        {
            get
            {
                if (this.PostTime == null)
                    return DateTime.MinValue;
                else
                    return (DateTime)this.PostTime;
            }
            set
            {
                DateTime result;

                if (DateTime.TryParse(value.ToString(), out result))
                {
                    PostTime = result;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("PostTime", value, "The post time was not a valid date time.");
                }
            }
        }

        public virtual bool HasCompletePayoutInfo
        {
            get
            {
                return this.GetPayout(BetTypes.Win) != null && this.GetPayout(BetTypes.Place) != null &&
                    this.GetPayout(BetTypes.Show) != null;
            }
        }

        public virtual IList<RaceDetail> RaceDetailsByPost
        {
            get
            {
                return this.RaceDetails.OrderBy(raceDetail => raceDetail.PostPosition).ToList();
            }
        }

        public virtual IList<RaceDetail> RaceDetailsByOdds
        {
            get
            {
                return RaceDetails.OrderBy(raceDetail => raceDetail.OddsOrder).ToList();
            }
        }

        public virtual RaceDetail Favorite
        {
            get
            {
                return RaceDetailsByOdds.Single(raceDetail => raceDetail.OddsOrder == 1);
            }
        }

        public virtual RaceDetailPayout Win
        {
            get
            {
                return this.GetPayout(BetTypes.Win);
            }
        }

        public virtual RaceDetailPayout Place
        {
            get
            {
                return this.GetPayout(BetTypes.Place);
            }
        }

        public virtual RaceDetailPayout Show
        {
            get
            {
                return this.GetPayout(BetTypes.Show);
            }
        }

        public virtual RaceDetailPayout GetFinishPosition(RaceDetail raceDetail)
        {
            if (Win.RaceDetail == raceDetail)
                return Win;

            if (Place.RaceDetail == raceDetail)
                return Place;

            if (Show.RaceDetail == raceDetail)
                return Show;

            return null;
        }

        public virtual LeagueRaceReport LeagueRaceReport
        {
            get
            {
                return new LeagueRaceReport(this.ReportLeagueRaceBets);
            }
        }
    }
}