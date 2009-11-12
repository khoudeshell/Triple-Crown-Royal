using System;
using System.Data;
using System.Web.Caching;


namespace HorseLeague.Cache
{
    public abstract class CacheKey
    {
        private readonly string _key;
        private readonly CacheDependency _dependency;
        private readonly TimeSpan _slidingExpiration;
        private readonly DateTime _absoluteExpiration;

        public CacheKey(string key, CacheDependency dependency, 
            DateTime absoluteExpiration) : this(key, dependency, 
            absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration) { }

        public CacheKey(string key, CacheDependency dependency, 
            DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            this._key = key;
            this._dependency = dependency;
            this._absoluteExpiration = absoluteExpiration;
            this._slidingExpiration = slidingExpiration;
        }

        public CacheDependency Dependency
        {
            get { return _dependency; }
        }

        public DateTime AbsoluteExpiration
        {
            get { return _absoluteExpiration; }
        }

        public TimeSpan SlidingExpiration
        {
            get { return _slidingExpiration; }
        } 

        public string Key
        {
            get { return this._key; }
        }
    }
}
