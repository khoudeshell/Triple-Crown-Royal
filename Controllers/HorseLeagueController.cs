using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using HorseLeague.Models.DataAccess;

namespace HorseLeague.Controllers
{
    public class HorseLeagueController : Controller
    {
        private readonly IHorseLeagueRepository _dataRepository;
        private IUserSecurity _convertor;
        
        public HorseLeagueController() : this(new HorseLeagueRepository()) { }

        public HorseLeagueController(IHorseLeagueRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        public IUserSecurity Convertor
        {
            get
            {
                if (this._convertor == null)
                {
                    _convertor = new UserSecurity(this.ControllerContext);
                }

                return _convertor;
            }
            set
            {
                _convertor = value;
            }
        }

        protected IHorseLeagueRepository Repository
        {
            get
            {
                return this._dataRepository;
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dataRepository.Dispose();
        }
    }
}
