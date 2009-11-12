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

namespace HorseLeague.Cache
{
    public class CacheFactory
    {
        private static volatile ICacheManager _cacheManager;
        private static object syncRoot = new Object();

        private CacheFactory() { }

        public static ICacheManager GetCacheManager()
        {
            if (_cacheManager == null)
            {
                lock (syncRoot)
                {
                    if (_cacheManager == null)
                        _cacheManager = new InMemoryCacheManager();
                }
            }

            return _cacheManager;
        }
    }
}
