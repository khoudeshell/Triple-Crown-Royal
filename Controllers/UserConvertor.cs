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
using System.Web.Mvc;

namespace HorseLeague.Controllers
{
    public class UserSecurity : HorseLeague.Controllers.IUserSecurity 
    {
        private readonly ControllerContext _context;

        public UserSecurity() { }

        public UserSecurity(ControllerContext context)
        {
            this._context = context;
        }

        public System.Guid UserId
        {
            get
            {
                return (System.Guid)Membership.GetUser(_context.HttpContext.User.Identity.Name).ProviderUserKey;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _context.HttpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}
