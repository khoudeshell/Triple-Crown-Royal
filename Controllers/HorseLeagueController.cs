using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;
using HorseLeague.Models.Domain;

namespace HorseLeague.Controllers
{
    public class HorseLeagueController : Controller
    {
        private readonly IUserRepository _userRepository;
        private User user;

        public HorseLeagueController() : this(null) { }

        public HorseLeagueController(IUserRepository dataRepository)
        {
            this._userRepository = new UserRepository();
        }

        protected User HorseUser
        {
            get
            {
                if(user == null)
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
