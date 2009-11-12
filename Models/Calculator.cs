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
    public abstract class Calculator
    {
        private RaceDetailPayout _payout;
        
        public RaceDetailPayout Payout
        {
            set
            {
                this._payout = value;
            }
        }

        public virtual double WinAmount
        {
            get
            {
                return Convert.ToDouble(_payout.WinAmount);
            }
        }

        public virtual double PlaceAmount
        {
            get
            {
                return Convert.ToDouble(_payout.PlaceAmount);
            }
        }

        public double ShowAmount
        {
            get
            {
                return Convert.ToDouble(_payout.ShowAmount);
            }
        }

        public double Total
        {
            get
            {
                return (WinAmount + PlaceAmount + ShowAmount) * _payout.LeagueRace.Weight;
            }
        }
    }

    public class WinCalculator : Calculator
    {
    }

    public class PlaceCalculator : Calculator
    {
        public override double WinAmount
        {
            get
            {
                return 0.0D;
            }
        }
    }

    public class ShowCalculator : Calculator
    {
        public override double WinAmount
        {
            get
            {
                return 0.0D;
            }
        }

        public override double PlaceAmount
        {
            get
            {
                return 0.0D;
            }
        }
    }


}
