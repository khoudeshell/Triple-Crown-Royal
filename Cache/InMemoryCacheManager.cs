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
    public class InMemoryCacheManager : ICacheManager
    {
        #region ICacheManager Members

        private const bool ENABLE_CACHE = true;
        public T Get<T>(CacheKey item, Func<T> callback)
        {

            if (!ENABLE_CACHE)
                return callback();

            T reqItem = (T) HttpRuntime.Cache.Get(item.Key);
            if (reqItem == null)  
            {
                reqItem = callback();
                Insert(item, reqItem);  
            }
            return reqItem;  
        }

        public void Insert(CacheKey item, object value)
        {
            HttpRuntime.Cache.Insert(item.Key, value, item.Dependency, item.AbsoluteExpiration, item.SlidingExpiration);
        }

        public void Remove(CacheKey item)
        {
            HttpRuntime.Cache.Remove(item.Key);
        }

        #endregion
    }
}
