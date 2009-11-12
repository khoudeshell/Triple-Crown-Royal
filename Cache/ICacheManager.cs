using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseLeague.Cache
{
    public interface ICacheManager
    {
        T Get<T>(CacheKey key, Func<T> callback);  
        void Insert(CacheKey key, object value);
        void Remove(CacheKey key);
    }
}
