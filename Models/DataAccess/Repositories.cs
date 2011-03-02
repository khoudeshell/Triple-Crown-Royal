using System;
using System.Collections.Generic;
using System.Linq;
using SharpArch.Data.NHibernate;
using HorseLeague.Models.Domain;
using SharpArch.Core.PersistenceSupport;
using NHibernate.Linq;
using System.Data;
using NHibernate.Criterion;
using NHibernate;

namespace HorseLeague.Models.DataAccess
{
    public class HorseLeagueRepository<T> : Repository<T>, IHorseLeagueRepository<T> { }

    public class UserRepository : RepositoryWithTypedId<User, Guid>, IUserRepository 
    {
        public User GetByUserName(string userName)
        {
            return this.Session
                    .CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("UserName", userName))
                    .UniqueResult<User>();
        }

        public IList<User> GetUsersWithScratches(LeagueRace leagueRace)
        {
            var scratches = this.Session.CreateSQLQuery(String.Format(@"SELECT u.* FROM aspnet_users u 
                INNER JOIN UserLeague ul on u.UserId = ul.UserId
                INNER JOIN UserRaceDetail urd ON ul.UserLeagueId = urd.UserLeagueId
                INNER JOIN RaceDetails rd on rd.RaceDetailId = urd.RaceDetailId 
                WHERE rd.IsScratched = 1 and rd.LeagueRaceId = {0}", leagueRace.Id))
            .AddEntity("u", typeof(User))
            .List<User>();

            if (scratches == null)
                scratches = new List<User>();

            return scratches;

        }
    }

    public class LeagueRepository : Repository<League>, ILeagueRepository 
    {
        public void RecalculateStandings(League league)
        {
            using (IDbCommand command = this.Session.Connection.CreateCommand())
            {
                command.CommandText = "CalculateUserTotals";
                command.CommandType = CommandType.StoredProcedure;
                IDbDataParameter leagueId = command.CreateParameter();
                leagueId.DbType = DbType.Int32;
                leagueId.ParameterName = "@LeagueId";
                leagueId.Value = league.Id;

                command.Parameters.Add(leagueId);
                this.Session.Transaction.Enlist(command);
                command.ExecuteNonQuery();
            }
        }

        public League GetDefaultLeague()
        {
            return this.Get(1);
        }
    }

    public class HorseRepository : Repository<Horse>, IHorseRepository
    {

        #region IHorseRepository Members

        public Horse GetHorseByName(string horseName)
        {
            return this.Session
                    .CreateCriteria(typeof(Horse))
                    .Add(Restrictions.Eq("Name", horseName))
                    .UniqueResult<Horse>();
        }

        #endregion
    }

    public interface IHorseLeagueRepository<T> : IRepository<T> { }

    public interface ILeagueRepository : IHorseLeagueRepository<League> 
    {
        void RecalculateStandings(League league);
        League GetDefaultLeague();
    }

    public interface IUserRepository : IRepositoryWithTypedId<User, Guid> 
    {
        User GetByUserName(string userName);
        IList<User> GetUsersWithScratches(LeagueRace leagueRace);
    }

    public interface IHorseRepository : IHorseLeagueRepository<Horse>
    {
        Horse GetHorseByName(string horseName);
    }
}