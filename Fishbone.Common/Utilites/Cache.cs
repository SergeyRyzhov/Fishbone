using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Fishbone.Common.Utilites
{
    public class CacheManager
    {
        private static HttpRuntime _httpRuntime;

        public static Cache Cache
        {
            get
            {
                EnsureHttpRuntime();
                return HttpRuntime.Cache;
            }
        }

        private static void EnsureHttpRuntime()
        {
            if (null == _httpRuntime)
            {
                try
                {
                    Monitor.Enter(typeof(CacheManager));
                    if (null == _httpRuntime)
                    {
                        // Create an Http Content to give us access to the cache.
                        _httpRuntime = new HttpRuntime();
                    }
                }
                finally
                {
                    Monitor.Exit(typeof(CacheManager));
                }
            }
        }
        private static Cache s_cache;

        public static Cache Instance
        {
            get { return s_cache ?? (s_cache = new Cache()); }
        }
    }
}
