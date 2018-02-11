using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using LoginetApi.Models.Common.Interfaces;

namespace LoginetApi.Models.Common
{
    public class Repository<Tkey,Tvalue>  where Tvalue : IRepositoryItem<Tkey>
    {
        private class Entry
        {
            public DateTime addDate = DateTime.MinValue;
            public Tvalue obj;

            public Entry(Tvalue value)
            {
                obj = value;
                addDate = DateTime.Now;
            }
        }

        public static object syncRoot = new object();
        private Dictionary<Tkey, Entry> dictionary = new Dictionary<Tkey, Entry>();

        public int UpdateCacheIntervalMinutes { get; set; }
        private DateTime lastUpdate;
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
        }
        


        public Repository()
        {
            dictionary = new Dictionary<Tkey, Entry>();
        }

        public Tvalue this[Tkey key]
        {
            get
            {
                lock (syncRoot)
                {
                    if(dictionary.ContainsKey(key))
                        return dictionary[key].obj;
                    return default(Tvalue);
                }
            }
        }
        public void Add(Tvalue value) 
        {
            lock (syncRoot) 
            {
                dictionary.Add(value.id, new Entry(value));
                lastUpdate = DateTime.Now;
            }
        }
    }
}