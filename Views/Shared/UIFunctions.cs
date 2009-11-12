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
using HorseLeague.Models;
using HorseLeague.Models.DataAccess;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text;

namespace HorseLeague.Views.Shared
{
    public class UIFunctions
    {
        public static IList<SelectListItem> PopulateHorseDropDown(IList<RaceDetail> raceDetails, IList<UserRaceDetail> userPicks,
            BetTypes betType, System.Guid userId)
        {
            UserRaceDetail userRaceDetail = GetUserSelection(userPicks, betType, userId);

            return PopulateHorseDropDown(raceDetails, (RaceDetail raceDetail) =>
            {
                if (userRaceDetail != null && userRaceDetail.RaceDetailId == raceDetail.RaceDetailId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public static IList<SelectListItem> PopulateHorseDropDown(LeagueRace leagueRace, Predicate<RaceDetail> selectionEval)
        {
            return PopulateHorseDropDown(leagueRace.RaceDetails.ToList(), selectionEval);
        }

        public static IList<SelectListItem> PopulateHorseDropDown(LeagueRace leagueRace)
        {
            return PopulateHorseDropDown(leagueRace, (RaceDetail raceDetail) =>
            {
                return false;
            });
        }

        public static IList<SelectListItem> PopulateHorseDropDown(IList<RaceDetail> raceDetails, Predicate<RaceDetail> selectionEval)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            //Add the default node
            SelectListItem item = new SelectListItem();
            item.Text = "";
            item.Value = "-1";
            items.Add(item);

            foreach (RaceDetail rd in raceDetails)
            {
                item = new SelectListItem();
                item.Text = FormatHorseNameForDisplay(rd.PostPosition, rd.Horse.Name);
                item.Value = rd.RaceDetailId.ToString();

                if (selectionEval(rd))
                {
                    item.Selected = true;
                }

                items.Add(item);
            }

            return items;
        }

        public static string FormatHorseNameForDisplay(int postPosition, string horseName)
        {
            return String.Format("{0} - {1}", postPosition, horseName);
        }

        public static string FormatHorseNameForDisplay(RaceDetail rd)
        {
            return FormatHorseNameForDisplay(rd.PostPosition, rd.Horse.Name);
        }

        public static UserRaceDetail GetUserSelection(IList<UserRaceDetail> userPicks, BetTypes betType, System.Guid userId)
        {
            return (from up in userPicks
                         where up.BetType == Convert.ToInt32(betType) && up.UserId == userId
                         select up).FirstOrDefault();
        }

        public static RaceDetailPayout GetPayout(BetTypes payoutType, LeagueRace leagueRace)
        {
            return new LeagueRaceDomain(leagueRace).GetPayout(payoutType);
        }
       
        public static RaceDetailPayout GetRaceDetailPayoutForAUserSelection(LeagueRace leagueRace, UserRaceDetail userSelection)
        {
            return (from rdp in leagueRace.RaceDetailPayouts
                                    where rdp.RaceDetailId == userSelection.RaceDetailId
                                    select rdp).FirstOrDefault();
        }

        public static string GetScratchString(LeagueRaceDomain leagueDomain)
        {
            IList<RaceDetail> scratches = leagueDomain.GetScratches();

            string scratchOutput = String.Empty;

            if (scratches != null)
            {
                for (int i = 0; i < scratches.Count; i++)
                {
                    if (i != 0)
                    {
                        scratchOutput += ", ";
                    }

                    scratchOutput = String.Concat(scratchOutput, FormatHorseNameForDisplay(scratches[i]));
                }
            }

            return scratchOutput;
        }

        public static string GetBetTypeValueFromPayout(double? amount)
        {
            return (amount == null || amount == 0.0D) ? "-" : amount.ToString();
        }

        public static BetTypes GetBetType(int betType)
        {
            return (BetTypes)Enum.Parse(typeof(BetTypes), betType.ToString());
        }

        public static string GetGraphUrl(IList<ReportLeagueRaceBet> curReport,  BetTypes betType, int imagePad)
        {
            float multiplier = 0.0f;

            StringBuilder builder = new StringBuilder(@"http://chart.apis.google.com/chart?chxt=y");

            //builder.Append(String.Format("&chtt={0}%20Picks", betType.ToString()));
            builder.Append(@"&cht=bhs&chxt=y");
            builder.Append("&chxl=0:");
            
            for(int i = curReport.Count - 1; i > -1; i--)
            {
                builder.Append(String.Format(@"|{0}", curReport[i].RaceDetail.Horse.Name.Replace(" ", "+")));
            }
            builder.Append(String.Format("&chs=300x{0}", imagePad * curReport.Count));
            builder.Append(@"&chco=034af3");
            
            builder.Append(@"&chd=t:");
            
            for(int i=0; i < curReport.Count; i++)
            {
                if (i == 0 && curReport[i].UserBetCount != 0)
                {
                    multiplier = 100 / curReport[i].UserBetCount;
                }

                if(i > 0)
                {
                    builder.Append(",");
                }
                builder.Append((curReport[i].UserBetCount * multiplier).ToString());
            }

            return builder.ToString();
        }

        public static string FormatReportPercent(double number)
        {
            return number == 0 ? "0.0" : (number * 100).ToString("##");
        }

        public static string FormatReportAverage(double number)
        {
            return (number).ToString("##0.00");
        }

        public static string FormatROI(double number)
        {
            return number < 0 ? String.Format("({0})", FormatReportPercent(number)) : FormatReportPercent(number);
        }
    }
}
