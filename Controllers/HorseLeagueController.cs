using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models.Domain;
using HorseLeague.Logger;

namespace HorseLeague.Controllers
{
    public class HorseLeagueController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        private User user;

        public HorseLeagueController() : this(null, null) { }

        public HorseLeagueController(IUserRepository dataRepository, ILogger logger)
        {
            this._userRepository = dataRepository ?? new UserRepository();
            this._logger = logger ?? new Logger.Logger();
        }

        protected User HorseUser
        {
            get
            {
                if (user == null)
                    user = this.ControllerContext.HttpContext.Items["USER"] as User;

                if (user == null)
                    user = this.UserRepository.GetByUserName(this.ControllerContext.HttpContext.User.Identity.Name);

                return user;
            }
        }

        protected UserLeague UserLeague
        {
            get
            {
                return HorseUser.UserLeagues[0];
            }
        }

     
        protected IUserRepository UserRepository
        {
            get
            {
                return this._userRepository;
            }
        }

        protected ILogger Logger { get { return _logger; } }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
