using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace HorseLeague.Models.DataAccess
{
    public interface IHorseLeagueRepository : IDisposable
    {
        IList<LeagueRace> GetActiveRaces();
        IList<LeagueRace> GetAllRaces();
        //IList<LeagueRace> GetUserResults(System.Guid userId);
        IList<UserRaceDetail> GetUserResults(Guid userId);
        IList<LeagueRace> GetResults();

        IList<UserRaceDetail> GetUserPicks(int leagueRaceId, System.Guid userId);
        IList<aspnet_User> GetAllUsers();
        IList<UserStanding> GetUserStandings(DateTime year);
        IList<aspnet_User> GetUsersWithScratches(int leagueRaceId);
        IList<RaceDetail> GetScratches(int leagueRaceId);
        IList<ReportLeagueRaceBet> GetLeagueRaceBetReport(int leagueRaceId, BetTypes payoutType);
        IList<ReportLeagueRaceBet> GetLeagueRaceBetReport(int leagueRacedId);

        aspnet_User GetUser(System.Guid userId);
        LeagueRace GetLeagueRace(int id);
        Horse GetHorse(string horseName);
        RaceDetail GetRaceDetail(int raceDetailId);
        RaceDetailPayout GetPayout(LeagueRace leagueRace, BetTypes payoutType);

        void PersistUserPicks(IList<UserRaceDetail> userPicks, int leagueRaceId, System.Guid userId);
        event EventHandler<UserPicksEventArgs> OnPicksSet;

        void PersistLeagueRace(LeagueRaceDomain leagueRace);
        event EventHandler<LeagueRaceEventArgs> OnLeagueRaceChange;

        void PersistRaceDetailPayout(RaceDetailPayout racePayout);

        Horse PersistHorse(Horse horse);

        RaceDetail PersistRaceDetail(RaceDetail raceDetail);
        RaceDetail UpdateRaceDetail(RaceDetail raceDetail);
        event EventHandler<RaceDetailEventArgs> OnRaceDetailChange;

        void RecalculateStandings();
        event EventHandler<UserStandingsEventArgs> OnStandingsRecalculated;

    }
}
