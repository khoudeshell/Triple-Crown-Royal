using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Configuration;

namespace HorseLeague.Controllers
{
    public class FileController : Controller
    {
        [Authorize]
        public ActionResult Get(string fileName)
        {
            string configPath = ConfigurationManager.AppSettings["fileConfig"];
            string filePath = this.Server.MapPath(this.Request.ApplicationPath);

            filePath = String.Format("{0}{1}{2}", filePath, configPath, fileName);
            return this.File(filePath, "application/pdf");
        }
    }
}
