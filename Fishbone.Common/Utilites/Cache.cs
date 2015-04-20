using System;
using System.Collections.Generic;

namespace Fishbone.Common.Utilites
{
    public class Cache : ICache
    {
        private static readonly Lazy<ICache> s_instance;
        private Dictionary<string, object> m_storage;

        public static ICache Instance
        {
            get { return s_instance.Value; }
        }

        static Cache()
        {
            s_instance = new Lazy<ICache>(() => (ICache)(new Cache()));
        }

        private Cache()
        {
            m_storage = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get { return Get<object>(key); }
            set { Insert(key, value); }
        }

        public void Clear()
        {
            m_storage.Clear();
        }

        public void Insert<T>(string key, T value)
        {
            m_storage.Add(key, value);
        }

        public T Get<T>(string key)
        {
            return m_storage.ContainsKey(key) ? (T)m_storage[key] : default(T);
        }
    }

    public interface ICache
    {
        void Insert<T>(string key, T value);

        T Get<T>(string key);
        object this[string key] { get; set; }
        void Clear();
    }
}
