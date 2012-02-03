using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpArch.Core.DomainModel;

namespace HorseLeague.Models.Domain
{
    public class UserStandings : Entity
    {
        public virtual UserLeague UserLeague { get; set; }
        public virtual League League { get; set; }
        public virtual DateTime Year { get; set; }
        public virtual float Total { get; set; }
	    public virtual DateTime UpdateDate { get; set; }
	    public virtual float WinWinPct { get; set; }
	    public virtual float WinPlacePct { get; set; }
	    public virtual float WinShowPct { get; set; }
	    public virtual float PlacePlacePct { get; set; }
	    public virtual float PlaceShowPct { get; set; }
	    public virtual float ShowShowPct { get; set; }
	    public virtual float WinWinAvg { get; set; }
	    public virtual float WinPlaceAvg { get; set; }
	    public virtual float WinShowAvg { get; set; }
	    public virtual float PlacePlaceAvg { get; set; }
	    public virtual float PlaceShowAvg { get; set; }
	    public virtual float ShowShowAvg { get; set; }
	    public virtual float WinFavPct { get; set; }
        public virtual float ROI { get; set; }
        public virtual int CurPosition { get; set; }
        public virtual int PrevPosition { get; set; }
        public virtual int StandingDelta { get { return PrevPosition - CurPosition; } }
    }
}